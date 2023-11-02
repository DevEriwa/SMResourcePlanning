using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Config
{
    public class GeneralConfiguration
    {
        public string AdminEmail { get; set; }
        public string SupportAdmin { get; set; }
        public string BuckSMSBaseURL { get; set; }
        public string BuckSMSEmail { get; set; }
        public string BuckSMSPassword { get; set; }
        public string SiteName { get; set; }
    }
}
