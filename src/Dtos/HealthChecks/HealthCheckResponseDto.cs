using System.Collections.Generic;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Common;

namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.HealthChecks
{
    public class HealthCheckResponseDto
    {
        public HealthCheckStatus Status { get; set; }
        public List<ErrorDto> Errors { get; set; }
    }
}
