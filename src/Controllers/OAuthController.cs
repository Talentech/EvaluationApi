using Microsoft.AspNetCore.Mvc;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Common;

namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Controllers
{
    /// <summary>
    /// This is a sample implementation of the OAuth controller the EvaluationApi expects partners to implement
    /// This can be hosted anywhere and doesn't have to be implemented in the API connector.
    /// </summary>
    public class OAuthController : Controller
    {
        /// <summary>
        /// This is the endpoint where users will authenticate and grant Talentech access to their account data at the Partner side.
        /// This is usually done by showing the user a page where they enter their credentials and grants consent
        /// Notes to partner developer:
        /// Validate the client_id. This must be added to the EvaluationApi's configuration for a given partner
        /// Validate the redirect_uri. This should be whitelisted in the partner's app to prevent open redirect attacks.
        /// The redirect_uri may in some cases be configured in the Partner's setup directly.
        /// If this is the case, then this parameter can be ignored, and the user should instead be redirected to the redirect_uri configured in the partner's setup
        /// </summary>
        /// <param name="client_id"></param>
        /// <param name="state"></param>
        /// <param name="redirect_uri"></param>
        /// <param name="response_type"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index(string client_id, string state, string redirect_uri, string response_type = "code")
        {
            var code = "A one-time-use, randomly generated authorization code";
            return Redirect($"{redirect_uri}?state={state}&code={code}");
        }

        /// <summary>
        /// Returns a token that uniquely identifies the customer's account at the Partner app. This will be stored by Talentech and used in all subsequent requests.
        /// The partner must validate all the supplied parameters before issuing a token.
        /// </summary>
        /// <param name="client_id"></param>
        /// <param name="client_secret"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<ExamplePartnerTokenDto> Token(string client_id, string client_secret, string code)
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