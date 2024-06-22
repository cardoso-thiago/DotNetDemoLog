using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting.Json;

namespace DotNetDemoLog
{
    public static class LogConfiguration
    {
        public static void ConfigureLogger(string configFilePath)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(configFilePath)
                .Build();

            var appSettings = configuration.GetSection("AppSettings").Get<AppSettings>();

            var applicationEnricher = new ApplicationEnricher(appSettings.Application);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.With(applicationEnricher)
                .WriteTo.Console(new JsonFormatter())
                .CreateLogger();
        }
    }

    public class ApplicationEnricher : ILogEventEnricher
    {
        private readonly string _application;

        public ApplicationEnricher(string application)
        {
            _application = application;
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("Application", _application));
        }
    }
}