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
    public class State : BaseModel
    {
        public int? CountryId { get; set; }
        [Display(Name = "Country")]
        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }
    }
}
