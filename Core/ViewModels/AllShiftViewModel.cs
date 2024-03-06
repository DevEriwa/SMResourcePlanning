using Core.ViewModels.Shift;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
	public class AllShiftViewModel
	{
	  public virtual List<AssignedShift>? AssignedShifts { get; set; }
	  public virtual List<ShiftTime>? ShiftTime { get; set; }
	  public virtual List<ShiftName>? ShiftNames { get; set; }
	}
}
