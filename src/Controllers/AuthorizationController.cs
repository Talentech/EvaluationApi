using Microsoft.AspNetCore.Mvc;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Config;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Authorization;

namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Controllers
{
    [Route("")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        /// <summary>
        /// Called when a customer's admin user revokes access given to the EvaluationApi
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        [HttpPost(Constants.PartnersUrls.DeauthorizationEndpoint)]
        [Produces("application/json")]
        public IActionResult Deauthorize(DeauthorizationRequestDto requestDto)
        {
            // Should perform necessary cleanup at the partner side.
            return Ok();
        }
    }
}