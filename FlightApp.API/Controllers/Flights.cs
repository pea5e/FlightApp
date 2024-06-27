using FlightApp.Core;
using FlightApp.Core.Models;
using FlightApp.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Flights : ControllerBase
    {
        private readonly IUnitOfWork _contextAccessor;

        public Flights(IUnitOfWork contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        [HttpPost]
        public IActionResult GetById(SessionDTO session)
        {
            List<Flight> flights = _contextAccessor.Flights.findAllBy(f => (f.SessionPilot.SessionId.Equals(session.SessionId)), new string[] { "SessionPilot", "From", "To" }).ToList();
            for (int indexer=0;indexer<flights.Count;indexer++)
            {
                flights.ElementAt(indexer).SessionPilot.Pilot = null;
                flights.ElementAt(indexer).SessionPilot.Flights = null;

            }
            return Ok(flights);
        }
    }
}
