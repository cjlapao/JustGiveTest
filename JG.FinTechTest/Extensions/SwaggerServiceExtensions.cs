using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using NSwag.AspNetCore;

namespace JG.FinTechTest.Extensions
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerDocument(x =>
            {
                x.GenerateEnumMappingDescription = true;
                x.PostProcess = document =>
                {
                    document.Info.Title = "Gift Aid Service";
                    document.Info.Version = "v1.0.0";
                    document.Info.Description = "Just Give Gift Aid API Service";
                    document.Info.TermsOfService = "None";
                    document.Info.Contact = new NSwag.SwaggerContact
                    {
                        Name = "Carlos Lapao",
                        Email = "cjlapao@gmail.com",
                    };

                    document.Info.License = new NSwag.SwaggerLicense
                    {
                        Name = "Under MIT"
                    };
                };
            });
            return services;
        }

        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUi3(x => x.DocExpansion = "None");
            return app;
        }
    }
}
