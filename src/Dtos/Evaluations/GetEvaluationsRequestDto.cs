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
        /// The schema for this is controlled by the Partner. The contents here is identical to what is returned from the OAuth token endpoint.
        /// </summary>
        public ExamplePartnerTokenDto Auth { get; set; }
    }
}
