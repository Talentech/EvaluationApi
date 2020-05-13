using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Config;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.AccessTokens;

namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Services.Clients.AccessTokens
{
    public class TokenClient : ITokenClient
    {
        private readonly TokenServerConfig _config;
        private readonly HttpClient _client;

        public TokenClient(TokenServerConfig config, HttpClient client)
        {
            _config = config;
            _client = client;
        }

        public async Task<string> RequestAccessTokenAsync()
        {
            var request = GetClientCredentialsTokenRequest();
            var tokenResponse = await _client.SendAsync(request);
            var tokenResponseString = await tokenResponse.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<TokenResponseDto>(tokenResponseString);
            return responseDto.AccessToken;
        }

        public bool IsAccessTokenValid(string accessToken)
        {
            if (accessToken == null)
            {
                return false;
            }

            var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(accessToken);
            return DateTime.UtcNow <= decodedToken.ValidTo;
        }

        private HttpRequestMessage GetClientCredentialsTokenRequest()
        {
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("client_id", _config.ClientId),
                new KeyValuePair<string, string>("client_secret", _config.ClientSecret),
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
            });

            return new HttpRequestMessage(HttpMethod.Post, _config.TokenEndpoint)
            {
                Content = formContent
            };
        }
    }
}
