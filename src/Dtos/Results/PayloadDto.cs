using System.Collections.Generic;

namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Results
{
    public class PayloadDto
    {
        public string Message { get; set; }
        public string Score { get; set; }
        public List<ResultUriDto> ReportUrls { get; set; }
    }
}
