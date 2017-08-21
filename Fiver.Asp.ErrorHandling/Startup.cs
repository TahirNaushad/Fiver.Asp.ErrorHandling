using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Diagnostics;

namespace Fiver.Asp.ErrorHandling
{
    public class Startup
    {
        public void ConfigureServices(
            IServiceCollection services)
        {
        }

        public void Configure(
            IApplicationBuilder app, 
            IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        var feature =
                            context.Features.Get<IExceptionHandlerFeature>();
                        var exception = feature.Error;
                        await context.Response.WriteAsync(
                            $"<b>Oops!</b> {exception.Message}");
                    });
                });
            }

            app.Run(async (context) =>
            {
                throw new ArgumentException("T must be set");
                await context.Response.WriteAsync("Hello Error Handling!");
            });
        }
    }
}
