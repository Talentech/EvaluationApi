using System.Threading.Tasks;

namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Services.Clients.AccessTokens
{
    public interface ITokenClient
    {
        Task<string> RequestAccessTokenAsync();
        bool IsAccessTokenValid(string accessToken);
    }
}
