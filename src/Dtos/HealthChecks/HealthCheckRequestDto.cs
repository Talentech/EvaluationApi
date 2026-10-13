using Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Common;

namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.HealthChecks
{
    public class HealthCheckRequestDto
    {
        /// <summary>
        /// Fixed token mode only: the same JSON your token endpoint returned.
        /// In Refresh token mode this is null and the access token comes in the Authorization: Bearer header.
        /// </summary>
        public ExamplePartnerTokenDto? Auth { get; set; }
    }
}
