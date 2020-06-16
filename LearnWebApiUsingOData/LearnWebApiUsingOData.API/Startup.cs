using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnWebApiUsingOData.API.Contexts;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OData.Edm;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace LearnWebApiUsingOData.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(AzureADDefaults.BearerAuthenticationScheme)
                .AddAzureADBearer(options => Configuration.Bind("AzureAd", options));

            services.AddDbContext<LearnWebApiUsingODataDBContext>();
            services.AddControllers(mvcOptions =>
                mvcOptions.EnableEndpointRouting = false);
            services.AddOData();
            NLogSettings();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            IEdmModel model = EdmModelBuilder.GetEdmModel();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMvc(routeBuilder =>
            {
                routeBuilder.Select().Expand().Filter().OrderBy();
                routeBuilder.MapODataServiceRoute("odata", "odata", model);
            });

            //app.UseEndpoints(endpoints =>
            //{
            //    //endpoints.MapControllers();
            //    endpoints.MapODataRoute("odataPrefix", "odata", model);
            //});
        }

        private void NLogSettings()
        {
            var conf = new LoggingConfiguration();
            var console = new ConsoleTarget("console");
            console.Layout = LogLayout;
            conf.AddTarget(console);
            conf.LoggingRules.Add(new LoggingRule("*", NLog.LogLevel.Trace, console));

            LogManager.Configuration = conf;
        }

        private static readonly string LogLayout = "${longdate}|${event-properties:item=Application}|${logger}|${uppercase:${level}}|${message} ${exception} ${exception:format=Message, Type, ToString:separator=*}";
    }
}
