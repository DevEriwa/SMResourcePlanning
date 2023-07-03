using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Enums.Resource_Planing;

namespace Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "First Name")]
        public virtual string? FirstName { get; set; }
        [Display(Name = "Last Name")]
        public virtual string? LastName { get; set; }
        [Display(Name = "Name")]
        [NotMapped]
        public string Name => FirstName + " " + LastName;
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [NotMapped]
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and Confirm Password must be the same. ")]
        public string? ConfirmPassword { get; set; }
        public string? Address { get; set; }
        public int? Age { get; set; }
        public string? ProfilePicture { get; set; }
        public bool DisplayOnRota { get; set; }
        public string? Nationality { get; set; }
        public string? JobTitle { get; set; }
        public string? mobile { get; set; }
        public string? Phone { get; set; }
		public Gender? GenderId { get; set; }
        public int? LocationId { get; set; }
        [ForeignKey("LocationId")]
        public virtual Location Location { get; set; }
        public int? DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public virtual Department Departments { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Religion { get; set; }
    }
}
