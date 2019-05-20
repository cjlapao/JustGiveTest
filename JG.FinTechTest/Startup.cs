using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JG.FinTechTest.Domain;
using JG.FinTechTest.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSwag.AspNetCore;
using Microsoft.EntityFrameworkCore;
using JG.FinTechTest.Models;

namespace JG.FinTechTest
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
            // Adding the swagger documentation from the extension
            services.AddSwaggerDocumentation();

            // Adding the Gift Calculator service
            services.AddScoped<IGiftAidCalculator, GiftAidCalculator>();

            // Adding SQL support to persist database changes
            services.AddDbContext<JGFinTechTestContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("JGFinTechTestContext")));
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressModelStateInvalidFilter = true;
                    options.SuppressUseValidationProblemDetailsForInvalidModelStateResponses = true;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseSwaggerDocumentation();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseMvc();
        }
    }
}
