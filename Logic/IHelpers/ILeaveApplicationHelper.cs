﻿using Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.IHelpers
{
    public interface ILeaveApplicationHelper
    {
        bool CreateLeave(LeaveViewModel leaveDetails);
    }
}