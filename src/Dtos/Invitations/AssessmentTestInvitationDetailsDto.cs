using System;

namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Invitations
{
    public class AssessmentTestInvitationDetailsDto
    {
        public Guid InvitationId { get; set; }
        public string TestId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PreferredLanguage { get; set; }
        public string NoteToCandidate { get; set; }
    }
}
