using FlightApp.Core;
using FlightApp.Core.Models;
using FlightApp.Core.Repositories;
using FlightApp.Data.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static System.Net.WebRequestMethods;

namespace FlightApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Checkings : ControllerBase
    {
        private readonly IUnitOfWork _contextAccessor;
        private IFDPStrategy strategy;

        public Checkings(IUnitOfWork contextAccessor)
        {
            _contextAccessor = contextAccessor;
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
        }

        [HttpPost("Schedule")]
        public async Task<IActionResult> GetSchedule(ReserveDTO data)
        {
            using (var client = new HttpClient())
            {
                string uri = "https://pea5e.pythonanywhere.com/flight/";
                var result = await client.GetStringAsync(uri + data.code);

                List<Flight> flights = JsonConvert.DeserializeObject<List<Flight>>(result);

                int checkHour = flights.First().Depart.Hour;
                int checkMinute = flights.First().Depart.Minute;
                Session session = _contextAccessor.Sessions.find(s => (s.SessionId.Equals(data.sessionId)));

                if (checkHour >= 2 && (checkHour < 6 || ( checkHour==6 && checkMinute==0 ) ))
                {
                    strategy = new IncludeWOCL();
                }
                else
                {
                    strategy = new ExcludeWOCL();
                }

                List<Flight> traffic = new List<Flight>();
                traffic.Add(flights.First());
                int index = 1; 
                while(index < flights.Count && strategy.CalculateFDP(traffic)) 
                {
                    traffic.Add(flights.ElementAt(index));
                }
                if(!strategy.CalculateFDP(traffic))
                {
                    traffic.Remove(traffic.Last());
                }
                int checkOutHour = traffic.Last().Arrive.Hour;
                int checkOUtMInute = traffic.Last().Arrive.Minute;

                if ( (checkOutHour < checkHour && checkOutHour >= 2 ) || ( checkOutHour >= 2 && checkHour <= 2))
                {
                    strategy = new EndsInWOCL();
                }
                if (!strategy.CalculateFDP(traffic))
                {
                    traffic.Remove(traffic.Last());
                }

                Checking check = new Checking();
                check.Id = 0;
                check.CheckIn = traffic.First().Depart;
                check.CheckOut = traffic.Last().Arrive;
                check.Localisation = traffic.First().From;
                check.Session = session;
                        
                foreach(Flight flight in traffic)
                {
                    flight.SessionPilot = session;
                    flight.Plane = data.code;
                    flight.From = _contextAccessor.Airports.find(s => (s.Code.Equals(flight.From.Code)));
                    flight.To = _contextAccessor.Airports.find(s => (s.Code.Equals(flight.To.Code)));
                    _contextAccessor.Flights.Add(flight);
                }
                _contextAccessor.Complete();
                return Ok();
            }
        }

    }

    public class ReserveDTO
    {
        public string sessionId { get; set; }
        public string code { get; set; }
    }
}
