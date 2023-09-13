using Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Core.ViewModels
{
    public class LeaveViewModel
    {
        public int id { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string leaveReason { get; set; }
        public string companyId { get; set; }
        public string staffName { get; set; }
        public decimal numberOfDays { get; set; }
        public string status { get; set; }
        public string approvedBy { get; set; }
        public string staffId { get; set; }
        public string leaveType { get; set; }
        public string staffEmail { get; set; }
        public int companyBranchId { get; set; }
    }

    
}
