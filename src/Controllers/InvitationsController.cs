using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Config;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Common;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Invitations;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Results;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Services;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Services.Encryption;

namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Controllers
{
    [Route("")]
    [ApiController]
    public class InvitationsController : ControllerBase
    {
        private readonly StatusUpdateService _service;
        private readonly IEncryptionService _encryptionService;

        public InvitationsController(StatusUpdateService service, IEncryptionService encryptionService)
        {
            _service = service;
            _encryptionService = encryptionService;
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
            var sourceSystem = requestDto.EvaluationDetails.SourceSystem;
            
            // Persist the source system's public key, if provided
            if (!string.IsNullOrEmpty(requestDto.EvaluationDetails.SourceSystemPublicKey))
            {
                _encryptionService.StoreSourceSystemPublicKey(
                    sourceSystem, requestDto.EvaluationDetails.SourceSystemPublicKey);
            }

            var reportUrls = new List<ResultUriDto>
            {
                new ResultUriDto()
                {
                    EncryptedFields = Enumerable.Empty<string>(),
                    Name = "Report",
                    Type = UriType.PdfDownload,
                    Uri = "https://some-document-store.local/candidate.pdf"
                }
            };
            
            // Encrypt PII sensitive fields
            if (_encryptionService.ShouldEncryptForSourceSystem(sourceSystem))
            {
                foreach (var reportUrl in reportUrls)
                {
                    reportUrl.Uri = _encryptionService.EncryptField(sourceSystem, reportUrl.Uri);
                    reportUrl.EncryptedFields = new[] {"Uri"};
                }
            }

            // If the Partner provides reference checks, swap the template parameter to ReferenceCheckInvitationDetailsDto
            await _service.SendUpdateToEvaluationApi(requestDto.EvaluationDetails.InvitationId.ToString(),
                new StatusUpdateDto
                {
                    InvitationId = requestDto.EvaluationDetails.InvitationId,
                    Description = "Some description",
                    Message = "Completed",
                    ReportUrls = reportUrls,
                    Score = "N/A",
                    Status = InvitationStatus.Completed,
                    ScoreProfiles = new List<ScoreProfileResultDto>()
                    {
                        new ScoreProfileResultDto()
                        {
                            Id = "total-score",
                            Score = 6.5f
                        },
                        new ScoreProfileResultDto()
                        {
                            Id = "sub-score-1",
                            Score = 8.0f
                        }
                    }
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