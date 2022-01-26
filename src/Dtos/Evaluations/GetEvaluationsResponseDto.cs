using System.Collections.Generic;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Common;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Evaluations.CustomFields;

namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Evaluations
{
    public class EvaluationDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<LanguageDto> Languages { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// Optional. Contains a list of partner specific configuration parameters for a given evaluation form.
        /// The ATSes will display these fields to the end user if the partner needs extra parameters that aren't included in the interface.
        /// </summary>
        public List<FieldDto> CustomFields { get; set; }
        
        /// <summary>
        /// Optional. Contains a list of score profile definitions.
        /// The ATSes will use this to look up metadata, such as name, when results are posted. 
        /// </summary>
        public List<ScoreProfileDefinitionDto> ScoreProfiles { get; set; }
    }
}
