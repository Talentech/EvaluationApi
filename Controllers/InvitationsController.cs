using Everest.SampleAssessment.AssessmentProviders.Interfaces;
using Everest.SampleAssessment.Models;
using Everest.SampleAssessment.Models.Enum;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Everest.SampleAssessment.Controllers
{
    [Route("v1/invitations")]
    [ApiController]

    public class InvitationsController : Controller
    {
        private readonly IApiClientCallback _apiClientCallback;
        public InvitationsController(IApiClientCallback assessmentProvider)
        {
            _apiClientCallback = assessmentProvider;
        }

        [HttpGet("{invitationId}/status")]
        public async Task<IActionResult> InvitationStatus(Guid invitationId, [FromBody] InvitationStatusRequest invitationStatusRequestDto)
        {
            if (invitationStatusRequestDto == null)
            {
                return BadRequest();
            }

            var invitationStatus = InvitationStatusTypes.New;

            //TODO Get this object from a json file
            string response = ReturnJsonInput();

            var jObject = (JObject)JsonConvert.DeserializeObject(response);

            var statusValue = jObject["Status"].Value<string>();

            if (statusValue.Equals("started", StringComparison.OrdinalIgnoreCase)) invitationStatus = InvitationStatusTypes.Started;
            if (statusValue.Equals("completed", StringComparison.OrdinalIgnoreCase)) invitationStatus = InvitationStatusTypes.Completed;

            var ApiResponse = new SampleAssessmentResponse<string>()
            {
                IsSuccess = response != null,
                StatusId = ((int)invitationStatus),
                StatusMessge = statusValue
            };

            return Ok(ApiResponse);

        }


        [HttpPost("{invitationId}/{sendInvitation}")]
        public async Task<IActionResult> CreateSendAssessment(Guid invitationId, bool sendInvitation, [FromBody] InvitationCreation creationInvitation)
        {
            if (creationInvitation == null)
            {
                return BadRequest();
            }

            if (invitationId != creationInvitation.AssessmentTestDetails.InvitationId)
            {
                return BadRequest();
            }

            int createSendResponse = 0;

            var ApiResponse = new SampleAssessmentResponse<string>()
            {
                IsSuccess = createSendResponse == 0,
                StatusId = createSendResponse == 0 ? (int)InvitationStatusTypes.New : 0, /*0 = None in the calling API Client*/
                StatusMessge = createSendResponse.ToString()
            };

            return Ok(ApiResponse);
        }


        private string ReturnJsonInput()
        {
            var responseFile = $"{AppDomain.CurrentDomain.BaseDirectory}\\MoqJsonResponses\\GetTestStatusJsonResponse.json";
            using (StreamReader r = new StreamReader(responseFile))
            {
                string json = r.ReadToEnd();
                return json;
            }
        }

    }
}