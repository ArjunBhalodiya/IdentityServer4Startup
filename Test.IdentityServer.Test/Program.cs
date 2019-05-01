using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Test.IdentityServer.Test
{
    class Program
    {
        private static async Task Main()
        {
            await new ClientCredentialsToken().GetToken("", null).ConfigureAwait(false);
            await new ResourceOwnerToken().GetToken("", null).ConfigureAwait(false);
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

            var tokenResponse = await client.RequestClientCredentialsTokenAsync(request);

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine("\n\n");

            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            var response = await apiClient.GetAsync($"{baseUrl}/identity");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }
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

            var tokenResponse = await client.RequestPasswordTokenAsync(request);

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine("\n\n");

            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            var response = await apiClient.GetAsync($"{baseUrl}/identity");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(JArray.Parse(content));
            }
        }
    }
}