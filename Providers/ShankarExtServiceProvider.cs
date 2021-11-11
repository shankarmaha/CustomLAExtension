using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs.Description;
using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Azure.Workflows.ServiceProviders.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomLAExtension.Providers
{
    [Extension("ShankarExtServiceProvider", configurationSection: "ShankarExtServiceProvider")]
    public class ShankarExtServiceProvider : IExtensionConfigProvider
    {
        public ShankarExtServiceProvider(
            ServiceOperationsProvider serviceOperationsProvider,
            ShankarExtServiceOperationProvider operationsProvider)
        {
            serviceOperationsProvider.RegisterService(serviceName: ShankarExtServiceOperationProvider.ServiceName, serviceOperationsProviderId: ShankarExtServiceOperationProvider.ServiceId, serviceOperationsProviderInstance: operationsProvider);
        }

        public void Initialize(ExtensionConfigContext context)
        {
            
        }
    }
}
