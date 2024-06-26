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

        [HttpGet]
        public IActionResult GetById()
        {
            return Ok(_contextAccessor.Flights.findById(1));
        }
    }
}
