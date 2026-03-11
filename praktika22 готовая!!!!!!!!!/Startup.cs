using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using praktika22.Data.DataBase;
using praktika22.Data.Interfaces;
using praktika22.Data.Mocks;
using praktika22.Data.Models;
using System.Collections.Generic;

namespace praktika22
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ICategorys, DBCategory>();
            services.AddTransient<IItems, DBItems>();

            services.AddMvc(option => option.EnableEndpointRouting = false);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
        public static List<ItemsBasket> BasketItem = new List<ItemsBasket>();
    }
}