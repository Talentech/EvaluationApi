using Everest.SampleAssessment.AssessmentProviders.Interfaces;
using Everest.SampleAssessment.Models;
using Everest.SampleAssessment.Models.Config;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Everest.SampleAssessment.AssessmentProviders
{
    public class ApiClientCallback : IApiClientCallback
    {
        private readonly ApiHttpClient _apiHttpClient;

        AssessmentApiConfiguration _apiClientConfiguration;

        public ApiClientCallback(IOptions<AssessmentApiConfiguration> apiClientConfiguration, ApiHttpClient apiHttpClient)
        {
            _apiHttpClient = apiHttpClient;
            _apiClientConfiguration = apiClientConfiguration.Value;
        }

        public async Task<Result> GetTestResult(Guid invitationId)
        {
            var response = await _apiHttpClient.GetApiObjectAsync<Result>($"{_apiClientConfiguration.BaseUrl}/results/{invitationId}", default(CancellationToken));

            return response;
        }

        public async Task<bool> SendTestCompletionResultCallback(Result ApiResponse)
        {
            var responseObjectJson = JsonConvert.SerializeObject(ApiResponse);

            var response = await _apiHttpClient.PostStringAsync<bool>($"{_apiClientConfiguration.BaseUrl}/results/{ApiResponse.InvitationId}", responseObjectJson, default(CancellationToken));

            return response;
        }
    }
}
