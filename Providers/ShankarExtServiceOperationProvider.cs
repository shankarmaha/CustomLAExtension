using Microsoft.Azure.Workflows.ServiceProviders.Abstractions;
using Microsoft.Azure.Workflows.ServiceProviders.WebJobs.Abstractions.Providers;
using Microsoft.WindowsAzure.ResourceStack.Common.Collections;
using Microsoft.WindowsAzure.ResourceStack.Common.Extensions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CustomLAExtension.Providers
{
    [ServiceOperationsProvider(Id = ShankarExtServiceOperationProvider.ServiceId, Name = ShankarExtServiceOperationProvider.ServiceName)]
    public class ShankarExtServiceOperationProvider : IServiceOperationsProvider
    {
        public const string ServiceName = "ShankarExt";

        public const string ServiceId = "/serviceProviders/ShankarExt";

        private readonly List<ServiceOperation> serviceOperationsList;

        private readonly InsensitiveDictionary<ServiceOperation> apiOperationsList;


        public ShankarExtServiceOperationProvider()
        {
            serviceOperationsList = new List<ServiceOperation>();
            apiOperationsList = new InsensitiveDictionary<ServiceOperation>();

            this.apiOperationsList.AddRange(new InsensitiveDictionary<ServiceOperation>
            {
                { "HelloWorld", HelloWorldApiOperationDataProvider.Operation },
            });

            this.serviceOperationsList.AddRange(new List<ServiceOperation>
            {
                {  HelloWorldApiOperationDataProvider.Operation.CloneWithManifest(HelloWorldApiOperationDataProvider.OperationManifest) }
            });
        }
        

        string IServiceOperationsProvider.GetBindingConnectionInformation(string operationId, InsensitiveDictionary<JToken> connectionParameters)
        {
            return ServiceOperationsProviderUtilities
                    .GetRequiredParameterValue(
                        serviceId: ServiceId,
                        operationId: operationId,
                        parameterName: "connectionString",
                        parameters: connectionParameters)?
                    .ToValue<string>();
        }

        IEnumerable<ServiceOperation> IServiceOperationsProvider.GetOperations(bool expandManifest)
        {
            return expandManifest ? serviceOperationsList : GetApiOperations();
        }

        ServiceOperationApi IServiceOperationsProvider.GetService()
        {
            return ShankarExtApiOperationsDataProvider.GetServiceOperationApi();
        }

        Task<ServiceOperationResponse> IServiceOperationsProvider.InvokeOperation(string operationId, InsensitiveDictionary<JToken> connectionParameters, ServiceOperationRequest serviceOperationRequest)
        {
            HttpResponseMessage response;

            ShankarExtParameters shankarExtParameters = new ShankarExtParameters(connectionParameters, serviceOperationRequest);

            using (var client = new HttpClient())
            {
                var content = new StringContent(shankarExtParameters.Content);
                response = client.PostAsync(shankarExtParameters.Url, content).Result;
            }

            return Task.FromResult((ServiceOperationResponse)new ShankarExtResponse(JObject.FromObject(new { message = "Message sent" }), System.Net.HttpStatusCode.OK));

        }

        private IEnumerable<ServiceOperation> GetApiOperations()
        {
            return this.apiOperationsList.Values;
        }

    }
}
