﻿using FlightApp.Core.Models;
using FlightApp.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Flights : ControllerBase
    {
        private readonly IBaseRepo<Flight> _contextAccessor;

        public Flights(IBaseRepo<Flight> contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        [HttpGet]
        public IActionResult GetById()
        {
            return Ok(_contextAccessor.getById(1));
        }
    }
}
