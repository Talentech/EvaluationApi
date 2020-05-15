using System.Threading.Tasks;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Results;

namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Services.Clients.EvaluationApi
{
    public interface IEvaluationApiClient
    {
        Task PostStatusUpdate(string accessToken, StatusUpdateDto dto);
    }
}
