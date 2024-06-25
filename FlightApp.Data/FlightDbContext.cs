using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightApp.Core.Models;

namespace FlightApp.Data
{
    public class FlightDbContext : DbContext
    {
        public FlightDbContext(DbContextOptions<FlightDbContext> options) : base(options)
        {
        }

        public DbSet<Airport> Airports { get; set; }
        public DbSet<Pilot> Pilots { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Checking> Checkings { get; set; }
    }
}
