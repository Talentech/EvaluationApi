using Everest.SampleAssessment.Models;
using System;
using System.Threading.Tasks;

namespace Everest.SampleAssessment.AssessmentProviders.Interfaces
{
    public interface IApiClientCallback
    {
        Task<Result> GetTestResult(Guid invitationId);

        /// <summary>
        /// Send the results posted back by the METIS Api to API Client i.e. the API who initiated the Assessment test creation request
        /// </summary>
        /// <param name="ApiResponse"></param>
        Task<bool> SendTestCompletionResultCallback(Result ApiResponse);
    }
}
