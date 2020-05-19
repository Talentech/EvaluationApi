using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Config;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Common;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.HealthChecks;

namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Controllers
{
    [Route("")]
    [ApiController]
    public class HealthChecksController : ControllerBase
    {
        /// <summary>
        /// The EvaluationApi will call this endpoint for each customer's integration at regular intervals.
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        [HttpPost(Constants.PartnersUrls.HealthCheckEndpoint)]
        [Produces("application/json")]
        public ActionResult<HealthCheckResponseDto> HealthCheck(HealthCheckRequestDto requestDto)
        {
            // The Partner is free to perform any checks that can be useful to relay to Talentech
            // This will enable us to indicate for customer users if an integration can be used or not.
            var response = new HealthCheckResponseDto
            {
                Status = HealthCheckStatus.Healthy,
                Errors = new List<ErrorDto>()
            };
            return Ok(response);
        }
    }
}