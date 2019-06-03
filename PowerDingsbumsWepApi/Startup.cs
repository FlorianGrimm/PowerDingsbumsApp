using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSwag;
using NSwag.AspNetCore;
using NSwag.SwaggerGeneration.Processors.Security;

namespace PowerDingsbumsWepApi {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            services.AddOpenApiDocument(document => {
                document.DocumentName = "OpenApi";
                this.ConfigureSwaggerDocumentSettings(document);
            });
            services.AddSwaggerDocument(document => {
                document.DocumentName = "Swagger";
                this.ConfigureSwaggerDocumentSettings(document);
            });
        }

        private void ConfigureSwaggerDocumentSettings(
            SwaggerDocumentSettings document) {
            document.Title = "PowerDingsbums";
            document.Description = "PowerDingsbums";
            document.Version = "1.0";
            //document.DefaultPropertyNameHandling = NJsonSchema.PropertyNameHandling.CamelCase;            
            // Add custom document processors, etc.
            //https://docs.microsoft.com/en-us/connectors/custom-connectors/create-web-api-connector
            // test this by creating a Connector
            document.DocumentProcessors.Add(
                new SecurityDefinitionAppender(
                    "AAD",
                    new string[] { "Dingsbums" },
                    new SwaggerSecurityScheme {
                        Type = SwaggerSecuritySchemeType.OAuth2,
                        Flow = SwaggerOAuth2Flow.AccessCode,
                        Name = "AAD",
                        AuthorizationUrl = "https://login.windows.net/common/oauth2/authorize",
                        TokenUrl = "https://login.windows.net/common/oauth2/token",
                        Description = "AAD"
                    }));
            // Post process the generated document
            //document.PostProcess = d => d.Info.Title = "Hello world!";
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
