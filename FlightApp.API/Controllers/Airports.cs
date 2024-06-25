using FlightApp.Core.Models;
using FlightApp.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Airports : ControllerBase
    {
        private readonly IBaseRepo<Airport> _contextAccessor;

        public Airports(IBaseRepo<Airport> contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_contextAccessor.getAll());
        }

        [HttpGet("id")]
        public IActionResult GetById()
        {
            return Ok(_contextAccessor.getById(2));
        }
    }
}
