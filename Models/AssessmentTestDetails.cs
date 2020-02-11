using System;

namespace Everest.SampleAssessment.Models
{
    public class AssessmentTestDetails
    {
        public Guid InvitationId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string LanguageCode { get; set; }
    }
}
