using Microsoft.AspNetCore.Mvc;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Config;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.HealthChecks;

namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Controllers
{
    [Route(Constants.PartnersUrls.HealthCheckExtension)]
    [ApiController]
    public class HealthChecksController : ControllerBase
    {

        [HttpPost("")]
        public ActionResult<HealthCheckResponseDto> HealthCheck(HealthCheckRequestDto requestDto)
        {
            //The EvaluationApi will call this endpoint for each customer's integration at regular intervals.
            //The Partner is free to perform any checks that can be useful to relay to Talentech
            //This will allow us to indicate for customer users if an integration can be used or not.
            return Ok();
        }
    }
}