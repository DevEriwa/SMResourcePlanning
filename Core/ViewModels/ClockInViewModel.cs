using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class ClockInViewModel
    {
        public RotaObject? rotaObject { get; set; }
        public StaffClockIn? PunchToday { get; set; }
        public string UserId { get; set; }
        public string? Location { get; set; } 
        public string? ShiftStart { get; set; } 
        public string? ShiftEnd { get; set; } 
        public string? ClockInTime { get; set; } 
        public string? ClockOutTime { get; set; } 

    }
}
