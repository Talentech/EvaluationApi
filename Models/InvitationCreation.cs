using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Everest.SampleAssessment.Models
{
    public class InvitationCreation
    {
        public AssessmentTestDetails AssessmentTestDetails { get; set; }

        public SampleAssessmentAuthentication ProviderConfiguration { get; set; }

        public SampleAssessmentTestConfiguration ProviderTestConfiguration { get; set; }
    }
}
