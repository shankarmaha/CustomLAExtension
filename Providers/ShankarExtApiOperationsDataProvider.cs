using Microsoft.Azure.Workflows.ServiceProviders.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomLAExtension.Providers
{
    public static class ShankarExtApiOperationsDataProvider
    {
        public static ServiceOperationApi GetServiceOperationApi()
        {
            return new ServiceOperationApi()
            {
                Name = "ShankarExt",
                Id = "/serviceProviders/ShankarExt",
                Type = DesignerApiType.ServiceProvider,
                Properties = new ServiceOperationApiProperties
                {
                    BrandColor = 4287090426,
                    IconUri = new Uri(CustomLAExtension.Properties.Resources.IconUri),
                    Description = "ShankarExt",
                    DisplayName = "ShankarExt",
                    Capabilities = new ApiCapability[] { ApiCapability.Actions },
                    ConnectionParameters = new ConnectionParameters
                    {
                        ConnectionString = new ConnectionStringParameters
                        {
                            Type = ConnectionStringType.SecureString,
                            ParameterSource = ConnectionParameterSource.AppConfiguration,
                            UIDefinition = new UIDefinition
                            {
                                DisplayName = "Connection String",
                                Description = "test connection string",
                                Tooltip = "test connection string",
                                Constraints = new Constraints
                                {
                                    Required = "true",
                                },
                            },
                        },
                    },
                },
            };
        }
    }
}
