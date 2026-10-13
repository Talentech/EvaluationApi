using System.Collections.Generic;

namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Evaluations.CustomFields
{
    public class SelectListDto : FieldDto
    {
        /// <summary>
        /// Required, may be empty. Without it the whole evaluation forms response is rejected.
        /// </summary>
        public List<OptionDto> Options { get; set; }

        /// <summary>
        /// Optional. The id of the option to show as selected.
        /// </summary>
        public string? Value { get; set; }
    }
}
