﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class PunchingViewModel
    {
        public int Id { get; set; }
        public int LocationId { get; set; }
        public string UserId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
