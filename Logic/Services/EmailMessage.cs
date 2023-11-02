using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public class EmailMessage
    {
        public EmailMessage()
        {
            ToAddresses = new List<EmailAddress>();
            FromAddresses = new List<EmailAddress>();
        }

        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public List<EmailAddress> ToAddresses { get; set; }

        public List<EmailAddress> FromAddresses { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }
    }
}
