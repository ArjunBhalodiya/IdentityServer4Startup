using Microsoft.AspNetCore.Builder;
using Test.IdentityServer.Utility.Services;

namespace Test.IdentityServer.Utility
{
    public static class IdentityConfiguration
    {
        public static ServicesMetadata Services { get; set; }

        public static IApplicationBuilder UseIdentityConfiguration(this IApplicationBuilder builder, 
                                                                        ServicesMetadata services)
        {
            Services = services;
            return builder;
        }
    }
}