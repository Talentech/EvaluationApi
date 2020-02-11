using System;

namespace Everest.SampleAssessment.Models.Enum
{
    public enum InvitationStatusTypes
    {
        //1 ,2,3 are not required here, as they are internal status before sending
        //None = 0,
        //Created = 1,
        //Sent = 2,
        /// <summary>
        /// New == NotStarted in calling API
        /// </summary>
        New = 3,
        Started = 4,
        Completed = 5
    }
}
