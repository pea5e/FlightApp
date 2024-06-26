using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightApp.Core;
using FlightApp.Core.Models;
using FlightApp.Core.Repositories;
using FlightApp.Data.Repos;

namespace FlightApp.Data
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly FlightDbContext _context;

        public IBaseRepo<Pilot> Pilots { get; private set; }

        public IBaseRepo<Flight> Flights { get; private set; }

        public IBaseRepo<Checking> Checkings { get; private set; }

        public IBaseRepo<Session> Sessions { get; private set; }

        public IBaseRepo<Airport> Airports { get; private set; }

        public UnitOfWork(FlightDbContext Context)
        {
            _context = Context;
            Pilots = new BaseRepo<Pilot>(_context);
            Flights = new BaseRepo<Flight>(_context);
            Checkings = new BaseRepo<Checking>(_context);
            Sessions = new BaseRepo<Session>(_context);
            Airports = new BaseRepo<Airport>(_context);

        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
