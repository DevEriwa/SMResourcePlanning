using Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Enums.Resource_Planing;
using System.Xml.Linq;

namespace Core.ViewModels
{
    public class UserViewModel
    {
        public virtual string? FirstName { get; set; }
        public string? Email { get; set; }
        public virtual string? LastName { get; set; }
        public string Name => FirstName + " " + LastName;
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public Guid? CompanyId { get; set; }
        public bool RememberPassword { get; set; }
        public string? Address { get; set; }
        public int Age { get; set; }
        public string? ProfilePicture { get; set; }
        public bool DisplayOnRota { get; set; }
        public string? Nationality { get; set; }
        public string? JobTitle { get; set; }
        public string? mobile { get; set; }
        public string? Phone { get; set; }
        public int GenderId { get; set; }
        public string Gender { get; set; }
        public int DepartmentId { get; set; }
        public string Department { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Title Title { get; set; }
        public string? Religion { get; set; }
        public Status Status { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
    }
}
