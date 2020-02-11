using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Everest.SampleAssessment.Models
{
    public class SampleAssessmentResponse<T>
    {
        public bool IsSuccess { get; set; }

        public int StatusId { get; set; }

        public string StatusMessge { get; set; }

        public T ReturnObject { get; set; }
    }
}
