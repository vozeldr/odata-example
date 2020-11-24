using System.Linq;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Microsoft.OData.Edm;
using Newtonsoft.Json;
using OdataExample.Data;
using OdataExample.Data.Models;

namespace OdataExample
{
    public class Startup
    {
        // ReSharper disable once NotAccessedField.Local
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(optionsBuilder => { optionsBuilder.UseInMemoryDatabase("imdb"); },
                ServiceLifetime.Singleton);

            services.AddMvc(options => options.EnableEndpointRouting = false)
                .AddNewtonsoftJson(
                    options =>
                    {
                        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                        options.SerializerSettings.MaxDepth = 2;
                    })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddOData();
            services.AddCors(options =>
            {
                options.AddPolicy("DEV",
                    builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });
            });

            services.AddOpenApiDocument(c =>
            {
                c.Title = "Example API";
                c.DocumentName = "v1";
                c.Version = "1.0.0";
                c.Description =
                    "OData-enabled example REST API.";
            });

            SetSwaggerFormatters(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors("DEV");
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseMvc(endpoints =>
            {
                endpoints.EnableDependencyInjection();

                endpoints.Expand()
                    .Select()
                    .Count()
                    .Filter()
                    .MaxTop(100)
                    .OrderBy()
                    .MapODataServiceRoute("odata", "odata", GetEdmModel());
            });

            app.UseOpenApi(c => { });
            app.UseSwaggerUi3(c => { });
            app.UseReDoc(c => { });
        }

        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EnableLowerCamelCase();
            builder.EntitySet<Customer>("Customers");
            builder.EntitySet<Order>("Orders");
            builder.EntitySet<OrderItem>("OrderItems");
            return builder.GetEdmModel();
        }

        private static void SetSwaggerFormatters(IServiceCollection services)
        {
            services.AddMvcCore(options =>
            {
                foreach (ODataOutputFormatter outputFormatter in options.OutputFormatters
                    .OfType<ODataOutputFormatter>()
                    .Where(formatter => formatter.SupportedMediaTypes.Count == 0))
                    outputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/odata"));

                foreach (ODataInputFormatter inputFormatter in options.InputFormatters
                    .OfType<ODataInputFormatter>()
                    .Where(formatter => formatter.SupportedMediaTypes.Count == 0))
                    inputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/odata"));
            });
        }
    }
}
