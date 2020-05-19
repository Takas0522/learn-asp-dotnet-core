using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnWebApiUsingOData.API.LogExtension
{
    public class EntityFrameworkLogger
    {
        // cf. http://thedatafarm.com/data-access/logging-in-ef-core-2-2-has-a-simpler-syntax-more-like-asp-net-core/
        // cf. https://stackoverflow.com/questions/52495087/how-to-log-entity-framework-core-operations-by-nlog
        public ILoggerFactory GetLoggerFactory()
        {
            // EFで実行されたSQLのログを吐き出す
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder =>
                builder.AddConsole()
                .AddFilter(DbLoggerCategory.Database.Command.Name,
                         LogLevel.Information));
            return serviceCollection.BuildServiceProvider()
                .GetService<ILoggerFactory>();
        }
    }
}
