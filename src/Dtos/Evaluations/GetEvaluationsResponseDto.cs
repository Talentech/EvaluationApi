using System.Collections.Generic;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Common;

namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Evaluations
{
    public class EvaluationDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<LanguageDto> Languages { get; set; }
        public string Description { get; set; }
    }
}
