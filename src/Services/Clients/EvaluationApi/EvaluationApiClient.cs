using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Config;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Results;

namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Services.Clients.EvaluationApi
{
    public class EvaluationApiClient : IEvaluationApiClient
    {
        private readonly EvaluationApiConfig _config;
        private readonly HttpClient _client;
        private readonly ILogger<EvaluationApiClient> _logger;

        public EvaluationApiClient(EvaluationApiConfig config, HttpClient client, ILogger<EvaluationApiClient> logger)
        {
            _config = config;
            _client = client;
            _logger = logger;
        }

        public async Task PostStatusUpdate(string accessToken, StatusUpdateDto dto)
        {
            if (_client.DefaultRequestHeaders.Contains("Authorization"))
                _client.DefaultRequestHeaders.Remove("Authorization");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var res = await _client.PostAsync(
                _config.ResultsEndpoint,
                new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json"));

            if (!res.IsSuccessStatusCode)
            {
                // 404 and 410 are final. Retry other failures later, with exponential backoff (see the README).
                _logger.LogError("Posting the status update for invitation {InvitationId} failed with {StatusCode}: {Body}",
                    dto.InvitationId, (int)res.StatusCode, await res.Content.ReadAsStringAsync());
            }
        }
    }
}
