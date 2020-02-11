using Everest.SampleAssessment.Models.Config;
using System.Net.Http;
using System.Threading.Tasks;

namespace Everest.SampleAssessment.Extensions
{
    public static class IdentityServerConfigurationExtensions
    {
        public async static Task<string> GetAccessToken(this IdentityServerConfiguration idpConfiguration, HttpClient httpClient)
        {
            var accessToken = await httpClient.GetAccessToken(idpConfiguration.IdentityServerUrl,
                idpConfiguration.IdentityServerClientId,
                idpConfiguration.ClientSecret,
                idpConfiguration.ApiScope);

            return accessToken;
        }
    }
}
