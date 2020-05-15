using System;
using System.Collections.Generic;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Common;

namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Results
{
        public class StatusUpdateDto
        {
            public Guid InvitationId { get; set; }
            public InvitationStatus Status { get; set; }
            public string Message { get; set; }
            public string Description { get; set; }
            public string Score { get; set; }
            public List<ResultUriDto> ReportUrls { get; set; }
    }
}
