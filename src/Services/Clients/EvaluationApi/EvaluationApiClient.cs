using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Config;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Results;

namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Services.Clients.EvaluationApi
{
    public class EvaluationApiClient : IEvaluationApiClient
    {
        private readonly EvaluationApiConfig _config;
        private readonly HttpClient _client;

        public EvaluationApiClient(EvaluationApiConfig config, HttpClient client)
        {
            _config = config;
            _client = client;
        }

        public async Task PostStatusUpdate(string accessToken, string invitationId, StatusUpdateDto dto)
        {
            await _client.PostAsync(_config.ResultsEndpoint(invitationId), new StringContent(JsonConvert.SerializeObject(dto)));
        }
    }
}
