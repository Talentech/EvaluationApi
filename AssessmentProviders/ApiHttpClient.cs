using Everest.SampleAssessment.Extensions;
using Everest.SampleAssessment.Models.Config;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Everest.SampleAssessment.AssessmentProviders
{
    public class ApiHttpClient
    {
        private readonly ILogger _logger;
        private readonly HttpClient _client;
        IdentityServerConfiguration _idpConfiguration;

        public ApiHttpClient(HttpClient httpClient, IOptions<IdentityServerConfiguration> idConfiguration)
        {
            _client = httpClient;
            _idpConfiguration = idConfiguration.Value;
        }

        public async Task<T> PostStringAsync<T>(string url, string jsonInput, CancellationToken token)
        {

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);


            request.Headers.Add("Accept", "application/json");

            request.Headers.Add("User-Agent", "HttpClientFactory-Post");

            request.Content = new StringContent(jsonInput, Encoding.UTF8, "application/json");


            var accessToken = await _idpConfiguration.GetAccessToken(_client);

            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _client.SendAsync(request, token);

            try
            {
                var result = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(result);
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> GetStringAsync(string url, CancellationToken token)
        {

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);


            try
            {
                var response = await _client.SendAsync(request, token);

                return await response.Content.ReadAsStringAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<T>> GetApiListAsync<T>(string apiUrl, CancellationToken token)
        {
            return await GetApiListAsync<T>(apiUrl, string.Empty, token);
        }

        public async Task<List<T>> GetApiListAsync<T>(string apiUrl, string jsonInput, CancellationToken token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);

            request.Headers.Add("Accept", "application/json");

            request.Headers.Add("User-Agent", "HttpClientFactory-Get");

            if (!string.IsNullOrWhiteSpace(jsonInput))
            {
                request.Content = new StringContent(jsonInput, Encoding.UTF8, "application/json");
            }


            var accessToken = await _idpConfiguration.GetAccessToken(_client);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            try
            {
                var response = await _client.SendAsync(request, token);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<List<T>>(result);
                }

                return default(List<T>);
            }
            catch
            {
                throw;

            }
        }

        public async Task<T> GetApiObjectAsync<T>(string apiUrl, CancellationToken token)
        {
            return await GetApiObjectAsync<T>(apiUrl, string.Empty, token);
        }

        public async Task<T> GetApiObjectAsync<T>(string apiUrl, string jsonInput, CancellationToken token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);

            request.Headers.Add("Accept", "application/json");

            request.Headers.Add("User-Agent", "HttpClientFactory-Get");

            if (!string.IsNullOrWhiteSpace(jsonInput))
            {
                request.Content = new StringContent(jsonInput, Encoding.UTF8, "application/json");
            }

            var accessToken = await _idpConfiguration.GetAccessToken(_client);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            try
            {
                var response = await _client.SendAsync(request, token);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<T>(result);
                }

                return default(T);
            }
            catch
            {
                throw;
            }

        }
    }
}
