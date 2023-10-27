using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Enums
{
	public class Resource_Planing
	{
		public enum Qualification
		{
			[Description("For Degree")]
			Degree = 1,
			[Description("For Higher National Diploma")]
			HND = 2,
			[Description("For National Diploma")]
			ND = 3,
			[Description("For Nigeria Certificate in Education")]
			NCE = 4,
		}

		public enum Title
		{
			[Description("For Mr")]
			Mr = 1,
			[Description("For Mrs")]
			Mrs,
			[Description("For Master")]
			Master,
			[Description("For Miss")]
			Miss,
		}
		public enum Gender
		{
			[Description("Male")]
			Male = 1,
			[Description("Female")]
			Female,
		}


		public enum MaritalStatus
		{
			[Description("For Sinlges")]
			Single = 1,
			[Description("For the Married")]
			Married = 2,
			[Description("For the Widowed")]
			Widowed = 3,
			[Description("For the Divorced")]
			Divorced = 4,

		}

		public enum MonthsOfTheYear
		{
			[Description("For January")]
			January = 1,
			[Description("For  Febuary")]
			Febuary = 2,
			[Description("For March")]
			March = 3,
			[Description("For April")]
			April = 4,
			[Description("For May")]
			May = 5,
			[Description("For  June")]
			June = 6,
			[Description("For July")]
			July = 7,
			[Description("For August")]
			August = 8,
			[Description("For September")]
			September = 9,
			[Description("For  October")]
			October = 10,
			[Description("For November")]
			November = 11,
			[Description("For December")]
			December = 12,
		}

		public enum ShiftStatus
		{
			[Description("For newly scheduled shift")]
			Scheduled = 1,
			[Description("For rejected shift")]
			Rejected,
			[Description("For completed shift")]
			Completed,
			[Description("For dropped shift")]
			Dropped,
		}

		public enum CompanySettings
		{
			[Description("To Show Client Module")]
			ShowAllClientsInAllScheduleScreen = 1,
			[Description("To Show Staff Module")]
			ShowAllStaffInAllScheduleScreen,
			[Description("To Show Leave Module")]
			ShowLeaveModule,
			[Description("To Show Clock Module")]
			ShowClockIn,
			[Description("To Show Payroll Module")]
			ShowPayroll,
			[Description("To Show Project managment Module")]
			ShowProject,
			[Description("To Show Schedule Module")]
			ShowSchedule,

		}

		public enum TaskPriority
		{
			[Description("For task with low priority")]
			Low = 1,
			[Description("For task with Normal priority")]
			Normal,
			[Description("For task with High priority")]
			High,
			[Description("For task with Urgent priority")]
			Urgent,
			[Description("For task with Immediate priority")]
			Immediate,
		}

		public enum StatusColour
		{
			[Description("Status Colour For Primary")]
			Primary = 1,
			[Description("Status Colour For Info")]
			Info,
			[Description("Status Colour For Danger")]
			Danger,
			[Description("Status Colour For Success")]
			Success,
			[Description("Status Colour For Warning")]
			Warning,
			[Description("Status Colour For Purple")]
			Purple,
		}

		public enum Status
		{
			[Description("FullTime")]
			Status = 1,
			[Description("PerTime")]
			Info,
			[Description("Half")]
			Danger,
			[Description("Status Colour For Success")]
			Success,
			[Description("Status Colour For Warning")]
			Warning,
			[Description("Status Colour For Purple")]
			Purple,
		}

        public enum LeaveStatus
        {
            [Description("For New")]
            Applied = 1,
            [Description("For Approved")]
            Approved,
            [Description("For Rejected")]
            Declined,
            [Description("For Cancel")]
            Cancel,
            [Description("For employee absence")]
            Absence,

        }

    }
}
