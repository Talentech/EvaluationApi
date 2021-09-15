using System.Collections.Generic;

namespace Talentech.EvaluationApi.SamplePartnerApiConnector.Dtos.Common
{
    public class ErrorDto
    {
        public ErrorType ErrorType { get; set; }
        public string Message { get; set; }

        public static List<ErrorDto> SampleErrorResponseBody()
        {
            return new List<ErrorDto>
            {
                new ErrorDto { ErrorType = ErrorType.SystemError, Message = "An technical error message of your choice that won't be shown to end users" },
                new ErrorDto { ErrorType = ErrorType.UserError, Message = "An end user friendly error message" }
            };
        }
    }
}
