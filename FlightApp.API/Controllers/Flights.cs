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
            return Ok(_contextAccessor.Flights.find(f => (f.SessionPilot.SessionId.Equals(session.SessionId))));
        }
    }
}
