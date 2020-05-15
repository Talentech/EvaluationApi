using System.Threading.Tasks;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Results;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Services.Clients.AccessTokens;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Services.Clients.EvaluationApi;

namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Services
{
    /// <summary>
    /// This is an example implementation of a service that can be used by partners to post invitation results to the EvaluationAPI
    /// </summary>
    public class StatusUpdateService
    {
        private string AccessToken { get; set; }
        private readonly ITokenClient _tokenClient;
        private readonly IEvaluationApiClient _evaluationApiClient;

        public StatusUpdateService(ITokenClient tokenClient, IEvaluationApiClient evaluationApiClient)
        {
            _tokenClient = tokenClient;
            _evaluationApiClient = evaluationApiClient;
        }

        public async Task SendUpdateToEvaluationApi(string invitationId, StatusUpdateDto dto)
        {
            if (!_tokenClient.IsAccessTokenValid(AccessToken))
            {
                AccessToken = await _tokenClient.RequestAccessTokenAsync();
            }

            await _evaluationApiClient.PostStatusUpdate(AccessToken, invitationId, dto);
        }
    }
}
