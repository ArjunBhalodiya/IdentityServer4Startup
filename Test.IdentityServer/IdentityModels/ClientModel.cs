using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace Test.IdentityServer.IdentityModels
{
    public class ClientModel
    {
        public string ClientId { get; set; }
        public string ClientSecrets { get; set; }
        public List<string> AllowedScopes { get; set; }
        public string AllowedGrantTypes { get; set; }
        public List<string> RedirectUris { get; set; }
        public List<string> PostLogoutRedirectUris { get; set; }
        public List<string> AllowedCorsOrigins { get; set; }
        public bool AllowOfflineAccess { get; set; }
        public bool RequirePkce { get; set; }
        public bool RequireClientSecret { get; set; }

        public ICollection<string> GetClientType()
        {
            switch (AllowedGrantTypes)
            {
                case "password":
                    return GrantTypes.ResourceOwnerPassword;
                case "password_client_credentials":
                    return GrantTypes.ResourceOwnerPasswordAndClientCredentials;
                case "authorization_code":
                    return GrantTypes.Code;
                case "authorization_code_client_credentials":
                    return GrantTypes.CodeAndClientCredentials;
                case "client_credentials":
                    return GrantTypes.ClientCredentials;
                case "implicit":
                    return GrantTypes.Implicit;
                case "implicit_client_credentials":
                    return GrantTypes.ImplicitAndClientCredentials;
                case "hybrid":
                    return GrantTypes.Hybrid;
                case "hybrid_client_credentials":
                    return GrantTypes.HybridAndClientCredentials;
                case "device_flow":
                    return GrantTypes.DeviceFlow;
                default:
                    throw new Exception($"Invalid grant type {AllowedGrantTypes}");
            }
        }
    }
}
