using System.Threading.Tasks;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Results;

namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Services.Clients.EvaluationApi
{
    public interface IEvaluationApiClient
    {
        Task PostEvaluationResult(string accessToken, string invitationId, ResultDto dto);
    }
}
