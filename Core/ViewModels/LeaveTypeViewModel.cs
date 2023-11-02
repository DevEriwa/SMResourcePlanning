using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Core.Models;

namespace Core.ViewModels
{
    public class LeaveTypeViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        [Display(Name = "Number Of Days")]
        public int NumberOfDays { get; set; }
        public int? ShiftId { get; set; }
        [Display(Name = "Shift")]
        [ForeignKey("ShiftId")]
        public virtual Shifts Shift { get; set; }
    }
}
