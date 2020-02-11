using Everest.SampleAssessment.AssessmentProviders.Interfaces;
using Everest.SampleAssessment.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Everest.SampleAssessment.Controllers
{
    [Route("v1/results")]
    public class ResultsController : Controller
    {
        private readonly IApiClientCallback _apiClientCallback;

        public ResultsController(IApiClientCallback assessmentProvider)
        {
            _apiClientCallback = assessmentProvider;
        }

        [HttpGet("{invitationId}")]
        public async Task<IActionResult> Get(Guid invitationId)
        {
             
            //Dummy response
            Result response = new Result()
            {
                AssessmentName = "Test",
                InvitationId = invitationId,
                PartnerId = new Guid("b983da37-179a-41ee-95eb-fb4c33a81d17"),
                ReportUrls = new List<Dictionary<string, string>>(),
                ReportUrlsAsCsv = "",
                Score = "4"
            };
             
            return Ok(response);
        }


        [HttpPost("{invitationId}")]
        public async Task<IActionResult> Post(Guid invitationId, [FromBody] Result testResult)
        {
            if (testResult == null)
            {
                return BadRequest();
            }

            if (invitationId != testResult.InvitationId)
            {
                return BadRequest();
            }

            foreach (var item in testResult.ReportUrls)
            {
                foreach (var dict in item)
                {
                    testResult.ReportUrlsAsCsv += dict.Value + "~" + dict.Key + ";";
                }
            }
            var isSuccess = await _apiClientCallback.SendTestCompletionResultCallback(testResult);

            return Ok(isSuccess);
        }
    }
    
}