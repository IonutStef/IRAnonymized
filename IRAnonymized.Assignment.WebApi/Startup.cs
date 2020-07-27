using AutoMapper;
using IRAnonymized.Assignment.Common.Settings;
using IRAnonymized.Assignment.Data.Contexts;
using IRAnonymized.Assignment.Data.Repositories;
using IRAnonymized.Assignment.Services;
using IRAnonymized.Assignment.Services.Interfaces;
using IRAnonymized.Assignment.WebApi.Extensions;
using IRAnonymized.Assignment.WebApi.Services.Settings;
using IRAnonymized.Assignment.WebApi.Services.Interfaces;
using IRAnonymized.Assignment.WebApi.Services;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Reflection;
using System.IO;

namespace IRAnonymized.Assignment.WebApi
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
            var optionsSection = Configuration.GetSection("appSettings");
            services.Configure<AppSettings>(optionsSection);
            var options = optionsSection.Get<AppSettings>();

            var mongoSection = Configuration.GetSection("mongoSettings");
            services.Configure<MongoDatabaseSettings>(mongoSection);

            var jsonSection = Configuration.GetSection("jsonSettings");
            services.Configure<JsonDatabaseSettings>(jsonSection);

            services.AddMvc()
                .AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore)
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // MassTransit
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://localhost/"),
                               h =>
                               {
                                   h.Username("guest");
                                   h.Password("guest");
                               });
            });

            services.AddSingleton<IPublishEndpoint>(busControl);
            services.AddSingleton<ISendEndpointProvider>(busControl);
            services.AddSingleton<IBus>(busControl);


            // Service Registration
            // Db Settings
            services.AddSingleton<IMongoDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<MongoDatabaseSettings>>().Value);
            services.AddSingleton<IJsonDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<JsonDatabaseSettings>>().Value);

            // Repositories
            services.AddTransient<IMongoContext, MongoContext>();
            services.AddTransient<IStoreRepository, StoreItemMongoRepository>();
            services.AddTransient<IStoreRepository, StoreItemJsonRepository>();

            // Services
            services.AddTransient<IStoreItemJsonService, StoreItemJsonService>();
            services.AddTransient<IStoreItemMongoService, StoreItemMongoService>();
            services.AddTransient<IImportService, ImportService>();

            services.AddTransient<IDownloadService, DownloadService>();
            services.AddTransient<IFileImportService, FileImportService>();
            services.AddTransient<IQueueService, QueueService>();
            //services.AddSingleton<IHostedService, DataInitializerHostedService>();

            // Mapping
            var mapperConfiguration = new MapperConfiguration(
                cfg =>
                {
                    cfg.AddMaps(typeof(FileImportService));
                    cfg.AddMaps(typeof(Startup));
                });
            services.AddSingleton<IMapper>(s => new Mapper(mapperConfiguration));

            // Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "IRAnonymized Assignment",
                    Version = "v1",
                    Description = "Demo api to import data from a CSV file"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });


            busControl.Start();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            app.ConfigureExceptionHandler();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "IRAnonymized Assignment V1");
            });

            app.UseMvc();
        }
    }
}