using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Core.Models
{
    public class ShiftsLocation: BaseModel
    {
        public int ShiftId { get; set; }
        [Display(Name = "Shift")]
        [ForeignKey("ShiftId")]
        public virtual Shifts Shift { get; set; }
        public int? StateId { get; set; }
        [Display(Name = "State")]
        [ForeignKey("StateId")]
        public virtual State State { get; set; }

        public string Address { get; set; }

        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [Display(Name = "Latitude")]
        public double Latitude { get; set; }

        [Display(Name = "Longitude")]
        public double Longitude { get; set; }

        [Display(Name = "CloclIn Radius")]
        public double AcceptedRadius { get; set; }
    }
}
