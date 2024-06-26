using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp.Core.Models
{
    public class Checking
    {
        public int Id { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public Airport Localisation { get; set; }
        [ForeignKey("SessionID")]
        public Session Session { get; set; }

    }
}
