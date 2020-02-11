using IdentityModel.Client;
using System.Net.Http;
using System.Threading.Tasks;

namespace Everest.SampleAssessment.Extensions
{
    public static class HttpClientExtensions
    {
        public async static Task<string> GetAccessToken(this HttpClient httpClient, string authority, string clientId, string clientSecret, string scope)
        {
            var response = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                //Address = "http://localhost:5010/connect/token",
                Address = $"{authority}/connect/token",
                ClientId = clientId,
                ClientSecret = clientSecret,
                Scope = scope
            });

            var accessToken = response.AccessToken;
            return accessToken;
        }
    }
}
