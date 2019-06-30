using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace Test.IdentityServer.Test1
{
    class Program
    {
        public static async Task Main()
        {
            await new ClientCredentialsToken().GetToken("http://localhost:30967", new ClientCredentialsTokenRequest
            {
                ClientId = "site.client.cred.dev",
                ClientSecret = "secret",
                Scope = "equitrix.usermanagement.api"
            }).ConfigureAwait(false);

            await new ResourceOwnerToken().GetToken("http://localhost:30967", new PasswordTokenRequest
            {
                ClientId = "site.password.dev",
                ClientSecret = "secret",
                UserName = "ArjunBhaldiya",
                Password = "Password",
                Scope = "equitrix.usermanagement.api"
            }).ConfigureAwait(false);
        }
    }

    internal class ClientCredentialsToken
    {
        internal async Task GetToken(string baseUrl, ClientCredentialsTokenRequest request)
        {
            var client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync(baseUrl);
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            request.Address = disco.TokenEndpoint;
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(request);

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);
        }
    }

    internal class ResourceOwnerToken
    {
        internal async Task GetToken(string baseUrl, PasswordTokenRequest request)
        {
            var client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync(baseUrl);
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            request.Address = disco.TokenEndpoint;
            var tokenResponse = await client.RequestPasswordTokenAsync(request);

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);
        }
    }
}