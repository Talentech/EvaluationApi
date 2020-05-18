using Microsoft.AspNetCore.Mvc;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Config;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Invitations;

namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Controllers
{
    [Route("")]
    [ApiController]

    public class InvitationsController : ControllerBase
    {
        /// <summary>
        /// This endpoint should transform and relay whatever information is needed to the Partner App.
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        [HttpPost(Constants.PartnersUrls.SendInvitationEndpoint)]
        [Produces("application/json")]
        public IActionResult CreateInvitation(InvitationDto<AssessmentTestInvitationDetailsDto> requestDto)
        {
            // If the Partner provides reference checks, swap the template parameter to ReferenceCheckInvitationDetailsDto
            return Ok();
        }
    }
}