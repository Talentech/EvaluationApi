using Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Common;

namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Evaluations
{
    public class GetEvaluationsRequestDto
    {
        /// <summary>
        /// Information about the ATS user who executes the request
        /// </summary>
        public TriggeredByDto TriggeredBy { get; set; }

        /// <summary>
        /// Fixed token mode only: the response of your token endpoint, sent back exactly as you returned it.
        /// In Refresh token mode this is null and the access token comes in the Authorization: Bearer header.
        /// </summary>
        public ExamplePartnerTokenDto? Auth { get; set; }

        /// <summary>
        /// The ATS project the forms are requested for, when the ATS sends one. Otherwise null.
        /// </summary>
        public string? ProjectId { get; set; }
    }
}
