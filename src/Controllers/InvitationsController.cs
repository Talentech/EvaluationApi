using Microsoft.AspNetCore.Mvc;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Config;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Invitations;

namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Controllers
{
    [Route(Constants.PartnersUrls.SendInvitationExtension)]
    [ApiController]

    public class InvitationsController : ControllerBase
    {

        /// <summary>
        /// This endpoint should transform and relay whatever information is needed by the Partner App.
        /// If the Partner provides reference checks, swap the template parameter to ReferenceCheckInvitationDetailsDto
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        [HttpPost("")]
        public IActionResult CreateInvitation(InvitationDto<ReferenceCheckInvitationDetailsDto> requestDto)
        {
            return Ok();
        }
    }
}