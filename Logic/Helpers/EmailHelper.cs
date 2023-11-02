using Core.Models;
using Logic.IHelpers;
using Logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Helpers
{
    public class EmailHelper : IEmailHelper
    {
        private readonly IEmailServices _emailServices;

        public EmailHelper(IEmailServices emailServices)
        {
            _emailServices = emailServices;
        }

        public bool SendAdminEmail(ApplicationUser user)
        {
            if (user != null)
            {
                string toEmail = user.Email;
                string subject = "Message From Emirate University.";

                var message = "Hello" + " " + user.FirstName + "," + " your registration on our website was successful." +
                    "</br> <br/> " + "Feel free to reach us, if u need any assistance " +
                    "</br> <br/> " + "Kind Regards," + "</br> <br/> " +


                    "Emirate University Group.";
                var isSent = _emailServices.SendEmail(toEmail, subject, message);
                if (isSent)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
