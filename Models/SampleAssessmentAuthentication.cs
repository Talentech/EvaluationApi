using Everest.SampleAssessment.Models.Enum;
using System;

namespace Everest.SampleAssessment.Models
{
    public class SampleAssessmentAuthentication
    {
        public Guid PartnerId { get; set; }
        public Guid ApiKey { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public EnvironmentTypes EnvironmentType { get; set; }
    }
}
