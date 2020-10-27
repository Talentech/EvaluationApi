using System.Collections.Generic;

namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Evaluations.CustomFields
{
    public class SelectListDto : FieldDto
    {
        public List<OptionDto> Options { get; set; }
    }
}
