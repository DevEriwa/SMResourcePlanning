using Core.Db;
using Core.Models;
using Core.ViewModels;
using Logic.IHelpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Helpers
{
    public class AdminHelper: IAdminHelper
    {
        private readonly AppDbContext _context;

        public AdminHelper(AppDbContext context)
        {
            _context = context;
        }

        public bool AddShiftLocation(ShiftLocationViewModel shiftDetails)
        {
            if (shiftDetails != null)
            {
                var shiftData = new ShiftsLocation()
                {
                    Active = true,
                    DeteCreated = DateTime.Now,
                    Deleted = false,
                    Name = shiftDetails.Name,
                    ShiftId = shiftDetails.ShiftId,
                    StateId = shiftDetails.StateId,
                    Address = shiftDetails.Address,
                    PostalCode = shiftDetails.PostalCode,
                };
                _context.ShiftsLocations.Add(shiftData);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<ShiftsLocation> GetShiftById(int shiftId)
        {
            try
            {
                if (shiftId > 0)
                {
                    var companyBranch = await _context.ShiftsLocations.Where(x => !x.Deleted && x.Id == shiftId).FirstOrDefaultAsync();
                    if (companyBranch != null)
                        return companyBranch;
                    return null;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
