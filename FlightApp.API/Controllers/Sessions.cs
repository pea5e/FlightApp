using FlightApp.Core;
using FlightApp.Core.Models;
using FlightApp.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Sessions : ControllerBase
    {
        private readonly IUnitOfWork _contextAccessor;

        public Sessions(IUnitOfWork contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        [HttpPost]
        public IActionResult GetPilotById(SessionDTO session)
        {
            Session s = _contextAccessor.Sessions.find(s => (s.SessionId.Equals(session.SessionId)), new string[] {"Pilot"});
            if(s != null)
            {
                s.Pilot.Sessions.Clear();
                return Ok(s.Pilot);
            }
            return Ok(new Session()); 
        }

    }

    public class SessionDTO
    {
        public string SessionId { get; set; }
    }
}
