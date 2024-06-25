using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp.Core.Models
{
    public class Pilot
    {
        public int Id { get; set; }

        [Required,MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(50)]
        public string Password { get; set; }

        public ICollection<Session> Sessions { get; } = new List<Session>();

        public ICollection<Checking> Checkings { get; } = new List<Checking>();



    }
}
