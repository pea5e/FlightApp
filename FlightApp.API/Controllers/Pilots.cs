using FlightApp.Core.Repositories;
using FlightApp.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FlightApp.Core;

namespace FlightApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Pilots : ControllerBase
    {
        private readonly IUnitOfWork _contextAccessor;

        public Pilots(IUnitOfWork contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }


        [HttpPost("register")]
        public IActionResult Register(Pilot p)
        {
            p.Email = p.Email.ToLower();
            IActionResult r =  Ok(_contextAccessor.Pilots.Add(p));
            _contextAccessor.Complete();
            return r;
        }

        [HttpPost("login")]
        public IActionResult Login(String Email,String Password)
        {
            Pilot pilot = _contextAccessor.Pilots.find(p => (  p.Email.Equals(Email.ToLower()) && p.Password.Equals(Password) ));
            
            Session s = new Session();
            if (pilot != null)
            {
                Random rnd = new Random();
                string sid = "";
                do
                {
                    sid = "";
                    for (int i = 0; i < 10; i++)
                    {
                        sid += rnd.Next(1, 10).ToString();
                    }
                } while (_contextAccessor.Sessions.find(s => (s.SessionId.Equals(sid))) != null);
                s.SessionId = sid;
                s.Pilot = pilot;
                _contextAccessor.Sessions.Add(s);
                _contextAccessor.Complete();
                s.Pilot.Sessions.Clear();
            }
            return Ok(s);
        }
    }
}
