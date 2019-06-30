using System;
using System.Collections.Generic;
using System.IO;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Test.IdentityServer.IdentityModels;

namespace Test.IdentityServer.App_Config
{
    public static class ConfigurationBuilder
    {
        public static List<Client> Clients { get; private set; }
        public static List<ApiResource> ApiResources { get; private set; }
        public static List<IdentityResource> IdentityResources { get; private set; }


        public static void Load(IHostingEnvironment environment)
        {
            var path = Path.Combine(environment.ContentRootPath, $"identityServer.config.{environment.EnvironmentName}.json");
            if (!File.Exists(path) && environment.IsDevelopment())
            {
                path = Path.Combine(environment.ContentRootPath, $"identityServer.config.json");
            }

            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"IdentityServer configuration file not found at: {path}");
            }

            var json = File.ReadAllText(path);

            try
            {
                var config = JsonConvert.DeserializeObject<ConfigurationModel>(json);
                SetClients(config);
                SetApiResources(config);
                SetIdentityResources(config);
            }
            catch (Exception ex)
            {
                throw new InvalidDataException($"Failed to deserialize IdentityServer configuration file: {ex.Message}");
            }
        }

        public static IEnumerable<Client> GetClients()
        {
            return Clients;
        }
        private static void SetClients(ConfigurationModel config)
        {
            Clients = new List<Client>();

            foreach (var client in config.Clients)
            {
                Clients.Add(new Client
                {
                    ClientId = client.ClientId,
                    ClientSecrets = { new Secret(client.ClientSecrets.Sha256()) },

                    AllowedScopes = client.AllowedScopes ?? new List<string>(),
                    AllowedGrantTypes = client.GetClientType(),

                    RedirectUris = client.RedirectUris ?? new List<string>(),
                    PostLogoutRedirectUris = client.PostLogoutRedirectUris ?? new List<string>(),
                    AllowedCorsOrigins = client.AllowedCorsOrigins ?? new List<string>(),

                    AllowOfflineAccess = client.AllowOfflineAccess,
                    RequirePkce = client.RequirePkce,
                    RequireClientSecret = client.RequireClientSecret,
                    Enabled = true
                });
            }
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return ApiResources;
        }
        private static void SetApiResources(ConfigurationModel config)
        {
            ApiResources = new List<ApiResource>();

            foreach (var apiResource in config.ApiResources)
            {
                ApiResources.Add(new ApiResource(apiResource.Name, apiResource.DisplayName, apiResource.ClaimTypes ?? null));
            }
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return IdentityResources;
        }
        private static void SetIdentityResources(ConfigurationModel config)
        {
            IdentityResources = new List<IdentityResource>();

            foreach (var identityResource in config.IdentityResources)
            {
                if (!Enum.TryParse(identityResource, out IdentityResourcesType identityResourcesType))
                {
                    throw new InvalidDataException($"Invalid Identity Resources Type in IdentityServer configuration.");
                }

                switch (identityResourcesType)
                {
                    case IdentityResourcesType.Address:
                        IdentityResources.Add(new IdentityResources.Address());
                        break;
                    case IdentityResourcesType.Email:
                        IdentityResources.Add(new IdentityResources.Email());
                        break;
                    case IdentityResourcesType.OpenId:
                        IdentityResources.Add(new IdentityResources.OpenId());
                        break;
                    case IdentityResourcesType.Phone:
                        IdentityResources.Add(new IdentityResources.Phone());
                        break;
                    case IdentityResourcesType.Profile:
                        IdentityResources.Add(new IdentityResources.Profile());
                        break;
                    default:
                        break;
                }
            }
        }
    }
}