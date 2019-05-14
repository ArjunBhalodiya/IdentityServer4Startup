using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Test.Identity.Api.Resource.Configuration;
using Test.IdentityServer.Utility;
using Test.IdentityServer.Utility.Middleware;

namespace Test.Identity.Api.Resource
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
            services.AddAuthentication("Bearer")
                    .AddIdentityServerAuthentication(options =>
                    {
                        options.Authority = "http://localhost:30967";
                        options.RequireHttpsMetadata = false;
                    });

            services.AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityMiddleware();
            app.UseIdentityConfiguration(new ApiResourceServicesMetadata
            {
                UserManagement = "http://localhost:30967"
            });

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}