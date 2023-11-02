using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Config
{
    public interface IGeneralConfiguration
    {
        string AdminEmail { get; set; }
        string SupportAdmin { get; set; }
        public string BuckSMSBaseURL { get; set; }
        public string BuckSMSEmail { get; set; }
        public string BuckSMSPassword { get; set; }
        string SiteName { get; set; }
    }
}
