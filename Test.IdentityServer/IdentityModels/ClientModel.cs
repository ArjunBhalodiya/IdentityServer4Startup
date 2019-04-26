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
    }
}
