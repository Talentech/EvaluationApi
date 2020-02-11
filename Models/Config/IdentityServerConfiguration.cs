using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Everest.SampleAssessment.Models.Config
{
    public class IdentityServerConfiguration
    {
        public string IdentityServerUrl { get; set; }
        public string IdentityServerClientId { get; set; }
        public string ClientSecret { get; set; }
        public string ApiScope { get; set; }
    }
}
