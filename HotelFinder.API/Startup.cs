using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelFinder.Business.Abstract;
using HotelFinder.Business.Concrete;
using HotelFinder.DataAccess.Abstract;
using HotelFinder.DataAccess.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HotelFinder.API
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(); //eklenmeli

            //dependency injection için eklenmeli
            services.AddSingleton<IHotelService, HotelManager>(); //IHotelService çaðýrýlýnca ctor'da HotelManager alýnsýn
            services.AddSingleton<IHotelRepository, HotelRepository>(); //IHotelRepository çaðýrýlýnca ctor'da HotelRepository alýnsýn

            //nswag.aspnetcore eklendi
            //services.AddSwaggerDocument();
            services.AddSwaggerDocument(config =>
            {  //title ve version ayarlar için
                config.PostProcess = (doc =>
                {
                    doc.Info.Title = "Hotels Api";
                    doc.Info.Version = "1.0.13";
                    doc.Info.Contact = new NSwag.OpenApiContact()
                    {
                        Name = "dasa",
                        Url = "asda"
                    };
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseOpenApi(); // for swagger
            app.UseSwaggerUi3(); //for swagger ui loclahost/swagger ile ui görülür

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});
                endpoints.MapControllers(); //eklendi
            });
        }
    }
}
