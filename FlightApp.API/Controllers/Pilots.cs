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

        [HttpGet]
        public IActionResult GetById() { 
            return Ok(_contextAccessor.Pilots.findById(1));
        }
    }
}
