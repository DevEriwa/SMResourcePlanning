using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.IHelpers
{
    public interface IEmailHelper
    {
        bool SendAdminEmail(ApplicationUser user);
    }
}
