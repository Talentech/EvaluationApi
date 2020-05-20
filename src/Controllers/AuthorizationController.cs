using Microsoft.AspNetCore.Mvc;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Config;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.AccessTokens;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Authorization;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Common;

namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Controllers
{
    [Route("")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        /// <summary>
        /// Users will be redirected to this endpoint when authorizing with the partner.
        /// This endpoint can either:
        /// 1. Serve a page where the user enters their credentials at the partner
        /// 2. Redirect user to a backend oauth server hosted by the partner
        /// </summary>
        /// <param name="client_id"></param>
        /// <param name="state"></param>
        /// <param name="redirect_uri"></param>
        /// <param name="response_type"></param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet(Constants.PartnerUrls.AuthorizationEndpoint)]
        public IActionResult Index(string client_id, string state, string redirect_uri, string response_type)
        {
            var code = "A one-time-use, randomly generated authorization code";
            return Redirect($"{redirect_uri}?state={state}&code={code}");
        }
        
        /// <summary>
        /// Called when a customer's admin user revokes access given to the EvaluationApi
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        [HttpPost(Constants.PartnerUrls.DeauthorizationEndpoint)]
        [Produces("application/json")]
        public IActionResult Deauthorize(DeauthorizationRequestDto requestDto)
        {
            // Should perform necessary cleanup at the partner side.
            return Ok();
        }

        /// <summary>
        /// Returns a token that uniquely identifies the customer's account at the Partner app. This will be stored by Talentech and used in all subsequent requests.
        /// </summary>
        /// <returns></returns>
        [HttpPost(Constants.PartnerUrls.TokenEndpoint)]
        [Consumes("application/x-www-form-urlencoded")]
        [Produces("application/json")]
        public ActionResult<ExamplePartnerTokenDto> Token([FromForm]TokenRequestDto request)
        {
            var token = new ExamplePartnerTokenDto
            {
                CustomerAccountId = "RandomAccountId",
                PartnerApiRefreshToken = "PartnerApiRefreshToken"
            };
            return Ok(token);
        }
    }
}