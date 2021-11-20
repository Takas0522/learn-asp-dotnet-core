using ApiConnectorSample.Utility;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: FunctionsStartup(typeof(ApiConnectorSample.Startup))]

namespace ApiConnectorSample
{
    public class Startup : FunctionsStartup
    {
        IConfiguration Configuration { get; set; }

        public override void Configure(IFunctionsHostBuilder builder)
        {
            var executionContextOptions = builder.Services.BuildServiceProvider()
                .GetService<IOptions<ExecutionContextOptions>>().Value;

            var currentDirectory = executionContextOptions.AppDirectory;
            var configuration = builder.Services.BuildServiceProvider().GetService<IConfiguration>();

            Configuration = new ConfigurationBuilder()
                .SetBasePath(currentDirectory)
                .AddConfiguration(configuration)
                .Build();

            builder.Services.AddSingleton<IClientCetificate, ClientCetificate>();

            builder.Services.AddSingleton(Configuration);

        }
    }
}
