using Core.Models;
using static Core.Enums.Resource_Planing;

namespace Logic.IHelpers
{
    public interface IDropdownHelper
	{
		 Task<List<CommonDropdowns>> GetDropdownsByKey(DropdownEnums dropdownEnums);

	}
}
