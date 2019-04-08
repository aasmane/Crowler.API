using Crowler.API.Providers;
using Crowler.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Crowler.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup()
        {
            //var builder = new ConfigurationBuilder()
            //   .AddJsonFile("appsettings.json");
            //Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton<ICrowlingProvider, IndeedProvider>();
            services.AddSingleton<ICrowlerService, CrowlerService>();
            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("CrowlerApi",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "First Api",
                        Version = "1.0"
                    });
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc(ConfigureRoutes);
            app.UseSwagger();
            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint("/swagger/CrowlerApi/swagger.json",
                    "First Api");
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            routeBuilder.MapRoute("Default", "{controller}/{action}/{id?}");
        }
    }
}
