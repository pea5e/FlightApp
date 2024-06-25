using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp.Core.Models
{
    public class Airport
    {
            public int Id { get; set; }
            public string Code { get; set; }
            public string City { get; set; }
            public string Country { get; set; }
    }
}
