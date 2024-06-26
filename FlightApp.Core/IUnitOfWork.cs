using FlightApp.Core.Models;
using FlightApp.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightApp.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepo<Pilot> Pilots { get; }
        IBaseRepo<Flight> Flights { get; }
        IBaseRepo<Checking> Checkings { get; }
        IBaseRepo<Session> Sessions { get; }
        IBaseRepo<Airport> Airports { get; }

        int Complete();

    }
}
