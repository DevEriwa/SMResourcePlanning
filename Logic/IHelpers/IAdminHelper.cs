using Core.Models;
using Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.IHelpers
{
    public interface IAdminHelper
    {
		bool AddShiftLocation(ShiftLocationViewModel shiftDetails);
        Task<ShiftsLocation> GetShiftById(int shiftId);

    }
}
