using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Test.IdentityServer.App_Config;

namespace Test.IdentityServer
{
    public class Startup
    {
        private readonly IHostingEnvironment environment;

        public Startup(IHostingEnvironment environment)
        {
            this.environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configure and add identity server services
            ConfigurationBuilder.Load(environment);
            services.AddIdentityServer()
                    .AddInMemoryIdentityResources(ConfigurationBuilder.GetIdentityResources())
                    .AddInMemoryApiResources(ConfigurationBuilder.GetApiResources())
                    .AddInMemoryClients(ConfigurationBuilder.GetClients())
                    .AddCustomUserStore()
                    .AddDeveloperSigningCredential();

            // Add MVC services
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            if (environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Use MVC services
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();

            // Use identity server services
            app.UseIdentityServer();
        }
    }
}
