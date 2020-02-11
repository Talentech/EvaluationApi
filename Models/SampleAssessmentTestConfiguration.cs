using System;
using System.ComponentModel.DataAnnotations;

namespace Everest.SampleAssessment.Models
{
    public class SampleAssessmentTestConfiguration
    {
        public string LanguageCode { get; set; }

        [Required]
        public string AssessmentName { get; set; }

        [Required]
        public Guid CriteriaId { get; set; }

        //optional
        public string CriteriaName { get; set; }

        //optional
        public string NormName { get; set; }

        //optional
        public Guid QuestionnaireId { get; set; }

        //optional
        public Guid ReportId { get; set; }
    }
}
