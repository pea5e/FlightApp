﻿using FlightApp.Core;
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
        public IActionResult GetPilotById(String SessionId)
        {
            Session s = _contextAccessor.Sessions.find(s => (s.SessionId.Equals(SessionId)), new string[] {"Pilot"});
            if(s != null)
            {
                s.Pilot.Sessions.Clear();
                return Ok(s.Pilot);
            }
            return Forbid(); 
        }


    }
}
