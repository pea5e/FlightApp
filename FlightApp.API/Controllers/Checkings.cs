using FlightApp.Core;
using FlightApp.Core.Models;
using FlightApp.Core.Repositories;
using FlightApp.Data.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using static System.Net.WebRequestMethods;

namespace FlightApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Checkings : ControllerBase
    {
        private readonly IUnitOfWork _contextAccessor;

        public Checkings(IUnitOfWork contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        [HttpGet]
        public IActionResult GetById()
        {
            return Ok(_contextAccessor.Checkings.findById(1));
        }


        [HttpGet]
        [Route("destinations/{airport}")]
        public async Task<IActionResult> GetDestinations(string airport)
        {
            using (var client = new HttpClient())
            {
                string uri = "https://pea5e.pythonanywhere.com/departure/";
                var result = await client.GetStringAsync(uri + airport);

                List<Flight> flights = JsonConvert.DeserializeObject<List<Flight>>(result);
                return Ok(flights);
            }
            return Ok(new List<Flight>());
        }
        
    }
}
