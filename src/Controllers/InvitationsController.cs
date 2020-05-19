using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Config;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Common;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Invitations;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Results;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Services;

namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Controllers
{
    [Route("")]
    [ApiController]

    public class InvitationsController : ControllerBase
    {
        private readonly StatusUpdateService _service;

        public InvitationsController(StatusUpdateService service)
        {
            _service = service;
        }

        /// <summary>
        /// This endpoint should transform and relay whatever information is needed to the Partner App.
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        [HttpPost(Constants.PartnersUrls.SendInvitationEndpoint)]
        [Produces("application/json")]
        public async Task<IActionResult> CreateInvitation(InvitationDto<AssessmentTestInvitationDetailsDto> requestDto)
        {
            // If the Partner provides reference checks, swap the template parameter to ReferenceCheckInvitationDetailsDto
            await _service.SendUpdateToEvaluationApi(requestDto.EvaluationDetails.InvitationId.ToString(), new StatusUpdateDto
            {
                InvitationId = requestDto.EvaluationDetails.InvitationId,
                Description = "Some description",
                Message = "Completed",
                ReportUrls = new List<ResultUriDto>(),
                Score = "N/A",
                Status = InvitationStatus.Completed
            });

            return Ok();
        }
    }
}