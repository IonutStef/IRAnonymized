using AutoMapper;
using IRAnonymized.Assignment.Common.Settings;
using IRAnonymized.Assignment.Consummers.Consummers;
using IRAnonymized.Assignment.Data.Contexts;
using IRAnonymized.Assignment.Data.Repositories;
using IRAnonymized.Assignment.Services;
using IRAnonymized.Assignment.Services.Interfaces;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using System;
using System.Threading.Tasks;

namespace IRAnonymized.Assignment.Consummers
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var hostBuilder = new HostBuilder()
                .ConfigureHostConfiguration(config => config.AddEnvironmentVariables())
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.SetBasePath(Environment.CurrentDirectory)
                          .AddJsonFile("appsettings.json", optional: false)
                          .AddEnvironmentVariables();
                })
                .ConfigureLogging((context, builder) =>
                {
                    var logger = new LoggerConfiguration()
                                .ReadFrom.Configuration(context.Configuration)
                                .CreateLogger();
                    builder.AddSerilog(logger);
                })
                .ConfigureServices((context, services) =>
                {
                    // Settings
                    var mongoSection = context.Configuration.GetSection("mongoSettings");
                    services.Configure<MongoDatabaseSettings>(mongoSection);

                    var jsonSection = context.Configuration.GetSection("jsonSettings");
                    services.Configure<JsonDatabaseSettings>(jsonSection);

                    // Mapping
                    var mapperConfiguration = new MapperConfiguration(
                        cfg =>
                        {
                            cfg.AddMaps(typeof(FileImportService));
                        });
                    services.AddSingleton<IMapper>(s => new Mapper(mapperConfiguration));

                    // Service Registration
                    services.AddSingleton<IMongoDatabaseSettings>(sp =>
                        sp.GetRequiredService<IOptions<MongoDatabaseSettings>>().Value);
                    services.AddSingleton<IJsonDatabaseSettings>(sp =>
                        sp.GetRequiredService<IOptions<JsonDatabaseSettings>>().Value);

                    // Repositories
                    services.AddTransient<IMongoContext, MongoContext>();

                    services.AddTransient<IStoreRepository, StoreItemMongoRepository>();
                    services.AddTransient<IStoreRepository, StoreItemJsonRepository>();

                    services.AddTransient<IFileImportService, FileImportService>();

                    // Build an intermediate service provider
                    var serviceProvider = services.BuildServiceProvider();

                    // Add MassTransit
                    var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
                    {
                        var host = cfg.Host(new Uri("rabbitmq://localhost/"), 
                            h =>
                            {
                                h.Username("guest");
                                h.Password("guest");
                            });

                        cfg.ReceiveEndpoint(host, "file_import_queue", e =>
                        {
                            var fileImportService = serviceProvider.GetRequiredService<IFileImportService>();
                            var loggingService = serviceProvider.GetRequiredService<ILogger<ImportFileConsummer>>();

                            e.Consumer(() =>
                                new ImportFileConsummer(fileImportService, loggingService));
                        });
                    });

                    services.AddSingleton<IPublishEndpoint>(busControl);

                    busControl.Start();
                });

            await hostBuilder.RunConsoleAsync();
        }
    }
}
