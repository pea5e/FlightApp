using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp.Core.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public string Ref { get; set; }
        public DateTime Depart { get; set; }
        public DateTime Arrive { get; set; }
        public string Plane { get; set; }
        public Session SessionPilot { get; set; }
        public Airport From { get; set; }
        public Airport To { get; set; }
        [NotMapped]
        public float TotalHeures { get; set; }
    }
}
