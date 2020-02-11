using System;
using System.Collections.Generic;

namespace Everest.SampleAssessment.Models
{
    public class Result
    {
        public Guid PartnerId { get; set; }

        public Guid InvitationId { get; set; } 

        public string AssessmentName { get; set; }

        public string Score { get; set; }

        public string ReportUrlsAsCsv { get; set; }

        public List<Dictionary<string, string>> ReportUrls { get; set; }
    }
}
