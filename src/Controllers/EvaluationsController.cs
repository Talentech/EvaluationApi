using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Config;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Evaluations;

namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Controllers
{
    [Route(Constants.PartnersUrls.AvailableTestsExtension)]
    [ApiController]

    public class EvaluationsController : ControllerBase
    {
        [HttpPost("")]
        public ActionResult<List<EvaluationDto>> GetEvaluations(GetEvaluationsRequestDto requestDto)
        {
            //This endpoint should return all the available criteria/questionnaires from the Partner's API
            //The requestDto should uniquely identify a customer's account at the Partner's side, and only data relevant for that customer should be returned
            return Ok();
        }
    }
}