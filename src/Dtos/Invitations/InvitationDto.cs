using Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Common;

namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Invitations
{
    public class InvitationDto<T>
    {
        /// <summary>
        /// The version of the invitation payload. 1 today.
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// Information about the ATS user who executes the request
        /// </summary>
        public TriggeredByDto TriggeredBy { get; set; }
        /// <summary>
        /// The type of this property should be either
        /// * AssessmentTestInvitationDetailsDto
        /// * ReferenceCheckInvitationDetailsDto
        /// * ContractInvitationDetailsDto
        /// * BackgroundCheckInvitationDetailsDto
        /// * FeedbackInvitationDetailsDto,
        /// depending on the type of service the Partner provides
        /// </summary>
        public T EvaluationDetails { get; set; }
        /// <summary>
        /// The schema for this is controlled by the Partner. The contents here is identical to what is returned from the OAuth token endpoint.
        /// </summary>
        public ExamplePartnerTokenDto Auth { get; set; }
    }
}
