using Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Common;

namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Results
{
        public class ResultDto
        {
            public PayloadType Type { get; set; }
            public string Status { get; set; }
            public PayloadDto Payload { get; set; }
        }
}
