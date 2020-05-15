﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Config;
using Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Evaluations;

namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Controllers
{
    [Route("")]
    [ApiController]

    public class EvaluationsController : ControllerBase
    {
        /// <summary>
        /// This endpoint should return all the available criteria/questionnaires from the Partner's API
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        [HttpPost(Constants.PartnersUrls.GetEvaluationFormsEndpoint)]
        public ActionResult<List<EvaluationDto>> GetEvaluationForms(GetEvaluationsRequestDto requestDto)
        {

            // The requestDto should uniquely identify a customer's account at the Partner's side, and only data relevant for that customer should be returned
            return Ok();
        }
    }
}