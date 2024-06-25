using FlightApp.Core.Repositories;
using FlightApp.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Pilots : ControllerBase
    {
        private readonly IBaseRepo<Pilot> _contextAccessor;

        public Pilots(IBaseRepo<Pilot> contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        [HttpGet]
        public IActionResult GetById() { 
            return Ok(_contextAccessor.getById(1));
        }
    }
}
