using System;
using System.Collections.Generic;
using System.Text;

[assembly: Microsoft.Azure.WebJobs.Hosting.WebJobsStartup(typeof(CustomLAExtension.Startup.ShankarExtStartup))]
namespace CustomLAExtension.Startup
{
    using CustomLAExtension.Providers;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Hosting;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    public class ShankarExtStartup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            builder.AddExtension<ShankarExtServiceProvider>();
            builder.Services.TryAddSingleton<ShankarExtServiceOperationProvider>();
        }
    }
}
