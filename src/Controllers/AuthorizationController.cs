using Microsoft.AspNetCore.Mvc;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Config;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Authorization;

namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Controllers
{
    [Route(Constants.PartnersUrls.DeauthorizationExtension)]
    [ApiController]

    public class AuthorizationController : ControllerBase
    {

        [HttpPost("")]
        public IActionResult Deauthorize(DeauthorizationRequestDto requestDto)
        {
            //If any steps should to be taken by the PartnerAPI, this is where to make that happen.
            return Ok();
        }
    }
}