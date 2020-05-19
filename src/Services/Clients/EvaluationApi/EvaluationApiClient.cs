using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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

        public async Task PostStatusUpdate(string accessToken, StatusUpdateDto dto)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var res = await _client.PostAsync(
                _config.ResultsEndpoint, 
                new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json"));
        }
    }
}
