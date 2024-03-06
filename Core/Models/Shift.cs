﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Shifts : BaseModel
    {
        public string? AbbreviatedName { get; set; }
        public bool? IsFixed { get; set; }
        public string? Activity { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public string? UnpaidTime { get; set; }
        public double? FixedAmount { get; set; }
        public double? HourlyPay { get; set; }
		public int? LocationId { get; set; }
		[ForeignKey("LocationId")]
		public virtual Location? Locations { get; set; }
        
        // the maximum staff allowed in a shift
        public int MaxStaff { get; set; }

        [NotMapped]
        public string User { get; set; }
        [NotMapped]
        public string UserId { get; set; }
        [NotMapped]
        public List<ApplicationUser> Users { get; set; }

        // Method to get the number of users in the shift
        public int GetNumberOfUsersInShift()
        {
            if (Locations != null && !string.IsNullOrEmpty(Locations.UserIds))
            {
                // Assuming UserIds is a comma-separated string
                string[] userIdsArray = Locations.UserIds.Split(',');

                // Filter out empty strings and count the number of user IDs
                return userIdsArray.Count(userId => !string.IsNullOrEmpty(userId));
            }

            // If Locations or UserIds is null or empty, return 0
            return 0;
        }

        // Method to get the number of users and the time shift
        public (int NumberOfUsers, string TimeShift) GetUserDataAndTimeShift()
        {
            if (Locations != null && !string.IsNullOrEmpty(Locations.UserIds))
            {
                // Assuming UserIds is a comma-separated string
                string[] userIdsArray = Locations.UserIds.Split(',');

                // Filter out empty strings and count the number of user IDs
                int numberOfUsers = userIdsArray.Count(userId => !string.IsNullOrEmpty(userId));

                // Construct the time shift
                string timeShift = $"{StartTime} - {EndTime}";

                return (numberOfUsers, timeShift);
            }

            // If Locations or UserIds is null or empty, return default values
            return (0, "N/A");
        }
    }
}
