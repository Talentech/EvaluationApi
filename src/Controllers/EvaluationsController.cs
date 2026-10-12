using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Config;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Common;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Evaluations;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Evaluations.CustomFields;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Services.Encryption;

namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Controllers
{
    [Route("")]
    [ApiController]

    public class EvaluationsController : ControllerBase
    {
        private readonly IEncryptionService _encryptionService;

        public EvaluationsController(IEncryptionService encryptionService)
        {
            _encryptionService = encryptionService;
        }

        /// <summary>
        /// This endpoint should return all the available criteria/questionnaires from the Partner's API
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        [HttpPost(Constants.PartnerUrls.GetEvaluationFormsEndpoint)]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<ErrorDto>), 500)]
        public ActionResult<List<EvaluationDto>> GetEvaluationForms(GetEvaluationsRequestDto requestDto)
        {
            // TriggeredBy can arrive encrypted here too, as on invitations.
            _encryptionService.Decrypt(requestDto.TriggeredBy);

            // The requestDto should uniquely identify a customer's account at the Partner's side, and only data relevant for that customer should be returned
            var res = new List<EvaluationDto>
            {
                new EvaluationDto
                {
                    Id = "id-from-partner-1",
                    Name = "BasicExample",
                    Description = "Basic example form",
                    Languages = new List<LanguageDto>(),
                    CustomFields = null
                },
                new EvaluationDto
                {
                    Id = "id-from-partner-2",
                    Name = "Custom Fields Example",
                    Description = "This is an example of an evaluation form which includes a list of custom fields",
                    Languages = new List<LanguageDto>(),
                    CustomFields = new List<FieldDto>
                    {
                        new SelectListDto
                        {
                            Id = "partner_specific_select_list",
                            Disabled = false,
                            Label = "Select list sent from the partner",
                            Type = FieldTypes.SelectList,
                            Options = new List<OptionDto>
                            {
                                new OptionDto
                                {
                                    Id = "option_1",
                                    Label = "Option 1",
                                }
                            }
                        },
                        new CheckboxDto
                        {
                            Id = "partner_specific_checkbox",
                            Disabled = false,
                            Label = "Checkbox sent from the partner",
                            Type = FieldTypes.Checkbox,
                            Value = false
                        }
                    },
                    ScoreProfiles = new List<ScoreProfileDefinitionDto>()
                    {
                        new ScoreProfileDefinitionDto()
                        {
                            Id = "total-score",
                            Name = "Total Score",
                            Description = "The overall score achieved by the candidate"
                        },
                        new ScoreProfileDefinitionDto()
                        {
                            Id = "sub-score-1",
                            Name = "Sub-score #1",
                            Description = "Some category specific score"
                        }
                    }
                }
            };

            return Ok(res);
        }
    }
}
