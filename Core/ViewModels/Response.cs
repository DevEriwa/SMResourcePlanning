using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class Response
    {

        public int Id { get; set; }
        public string? Msg { get; set; }
        public bool Status { get; set; }
        public bool isError { get; set; } = true;
        public object data { get; set; }
    }
}
