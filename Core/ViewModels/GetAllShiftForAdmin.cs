using Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class GetAllShiftForAdmin
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? RotaObjectString { get; set; }
        public string? Year { get; set; }
        public string? ShowAddBTN { get; set; }
        public string? TotalPlannedHour { get; set; }
        public string? DateRange { get; set; }
        public string? AbbreviatedName { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public int? LocationIds { get; set; }
        public int MaxStaff { get; set; }
        public string? Name { get; set; }
        public string User { get; set; }
        public List<Shifts> Shifts { get; set; }
        public List<ApplicationUser> Users { get; set; }
        public int? LocationId { get; set; }
        [ForeignKey("LocationId")]
        public virtual Location? Locations { get; set; }
        public int NumberOfUsers { get; set; }
        public string TimeShift { get; set; }
        public string ShiftTableHtml { get; set; }
    }
}
