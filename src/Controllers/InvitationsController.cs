using System.Collections.Generic;
using System.Linq;
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
        [HttpPost(Constants.PartnerUrls.SendInvitationEndpoint)]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<ErrorDto>), 500)]
        public async Task<IActionResult> CreateInvitation(InvitationDto<AssessmentTestInvitationDetailsDto> requestDto)
        {
            // If the Partner provides reference checks, swap the template parameter to ReferenceCheckInvitationDetailsDto
            await _service.SendUpdateToEvaluationApi(requestDto.EvaluationDetails.InvitationId.ToString(),
                new StatusUpdateDto
                {
                    InvitationId = requestDto.EvaluationDetails.InvitationId,
                    Description = "Some description",
                    Message = "Completed",
                    ReportUrls = new List<ResultUriDto>
                    {
                        new ResultUriDto()
                        {
                            EncryptedFields = Enumerable.Empty<string>(),
                            Name = "Report",
                            Type = UriType.PdfDownload,
                            Uri = "https://some-document-store.local/candidate.pdf"
                        }
                    },
                    Score = "N/A",
                    Status = InvitationStatus.Completed
                });

            return Ok();
        }

        /// <summary>
        /// In the event of an http error code being returned from your api, we will try to deserialize the response body.
        /// An example response can be seen below.
        /// </summary>
        private IActionResult SampleErrorResponse(int statusCode)
        {
            return StatusCode(statusCode, ErrorDto.SampleErrorResponseBody());
        }
    }
}