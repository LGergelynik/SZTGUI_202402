using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using QV596H_HFT_2023241.Endpoint.Services;
using QV596H_HFT_2023241.Logic;
using QV596H_HFT_2023241.Models;
using QV596H_HFT_2023241.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QV596H_HFT_2023241.Endpoint
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<CarDbContext>();
            services.AddTransient<IRepository<Car>,CarRepository>();
            services.AddTransient<IRepository<Brands>, BrandRepository>();
            services.AddTransient<IRepository<Rental>, RentalRepository>();
            services.AddTransient<ICarLogic,CarLogic>();
            services.AddTransient<IRentalLogic,RentalLogic>();
            services.AddTransient<IBrandsLogic,BrandsLogic>();

            services.AddSignalR();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "QV596H_HFT_2023241.Endpoint", Version = "v1" });
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "QV596H_HFT_2023241.Endpoint v1"));
            }

            app.UseExceptionHandler(c => c.Run(async context =>
            {
                var exception = context.Features
                    .Get<IExceptionHandlerPathFeature>()
                    .Error;
                var response = new { Msg = exception.Message };
                await context.Response.WriteAsJsonAsync(response);
            }));

            app.UseCors(x => x.AllowCredentials().AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:39915"));


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<SignalRHub>("/hub");
            });
        }
    }
}
