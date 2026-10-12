using System;
using System.Collections.Generic;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Common;

namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Results
{
        public class StatusUpdateDto
        {
            /// <summary>
            /// Required. The InvitationId from the invitation.
            /// </summary>
            public Guid InvitationId { get; set; }

            /// <summary>
            /// Required. Passed to the ATS as it is.
            /// </summary>
            public InvitationStatus Status { get; set; }

            public string Message { get; set; }
            public string Description { get; set; }

            /// <summary>
            /// Sent as a JSON string. A JSON number is rejected with 400.
            /// </summary>
            public string Score { get; set; }
            public List<ResultUriDto> ReportUrls { get; set; }
            public List<ScoreProfileResultDto> ScoreProfiles { get; set; }
    }
}
