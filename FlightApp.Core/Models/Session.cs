using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp.Core.Models
{
    public class Session
    {
        public int Id { get; set; }
        public string SessionId { get; set; }
        public Pilot Pilot { get; set; }
        public ICollection<Flight> Flights { get; set; }
    }
}
