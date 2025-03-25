using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using Microsoft.EntityFrameworkCore;
using api.leads.Data;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System;
using api.leads.Data.Services.Interface;
using api_leads.Data.Services.Implementation;
using api.leads.Data.Services.Implementation;
using api.leads.Data.Repositories;
using api.leads.MapperProfileConfiguration;
using Microsoft.Extensions.Hosting;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;

namespace api.leads
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
            services.AddControllers();
            // Cors Setup - Marketing
            services.AddCors();
            // Auto Mapper Configurations
            services.AddAutoMapper(typeof(MapperProfile));
            //database
            var connectionString = Configuration.GetValue<string>("LeadsApiConnection");
            services.AddSingleton(Configuration);
            services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));
            services.AddTransient<DbContext, DataContext>();
            services.AddTransient<ILeadsRepository, LeadsRepository>();
            services.AddTransient<ICampaignsRepository, CampaignsRepository>();
            services.AddTransient<IChargesRepository, ChargesRepository>();
            services.AddTransient<IEntryRepository, EntryRepository>();
            services.AddTransient<IFormsRepository, FormsRepository>();
            services.AddTransient<IAddressService, AddressService>();
            services.AddTransient<IDistributorService, DistributorService>();
            services.AddTransient<IFacebookService, FacebookService>();
            services.AddTransient<ISalesforceService, SalesforceService>();
            services.AddHttpClient<IHttpClientService, ApiManClientService>();

            // Register the Application Insights service
            services.AddApplicationInsightsTelemetry(options: new ApplicationInsightsServiceOptions
            {
                ConnectionString = Configuration.GetValue<string>("cluster-api-app-insights-connection-string")
            });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LeadsAPI", Version = "v1", Description = "Leads API" });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "LeadsAPI");
            });


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseCors();
        }
    }
}
