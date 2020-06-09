using Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Common;

namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Authorization
{
    public class DeauthorizationRequestDto
    {        
        /// <summary>
        /// Information about the ATS user who executes the request
        /// </summary>
        public TriggeredByDto TriggeredBy { get; set; }

        /// <summary>
        /// The schema for this is controlled by the partner. The contents here is identical to what is returned from the OAuth token endpoint.
        /// </summary>
        public ExamplePartnerTokenDto Auth { get; set; }

        /// <summary>
        /// The client id given to Talentech by the partner
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// The client secret given to Talentech by the partner
        /// </summary>
        public string ClientSecret { get; set; }
    }
}
