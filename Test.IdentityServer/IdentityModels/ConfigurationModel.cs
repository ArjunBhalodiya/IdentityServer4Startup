using System.Collections.Generic;

namespace Test.IdentityServer.IdentityModels
{
    public class ConfigurationModel
    {
        public List<ClientModel> Clients { get; set; }
        public List<ApiResourcesModel> ApiResources { get; set; }
        public List<string> IdentityResources { get; set; }

        public ConfigurationModel()
        {
            Clients = new List<ClientModel>();
            ApiResources = new List<ApiResourcesModel>();
            IdentityResources = new List<string>();
        }
    }
}
