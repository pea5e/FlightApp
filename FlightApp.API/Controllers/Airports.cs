using FlightApp.Core;
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
        private readonly IUnitOfWork _contextAccessor;

        public Airports(IUnitOfWork contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_contextAccessor.Airports.findAll());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_contextAccessor.Airports.findById(id));
        }
    }
}
