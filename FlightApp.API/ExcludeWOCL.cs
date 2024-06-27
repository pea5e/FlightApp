using FlightApp.Core.Models;

namespace FlightApp.API
{
    public class ExcludeWOCL : IFDPStrategy
    {
        private double MaxHours = 13;
        public bool CalculateFDP(List<Flight> flights)
        {
            MaxHours -= (flights.Count-1)*0.5;
            double sumHours = 0;
            foreach (Flight flight in flights)
            {
                sumHours += flight.TotalHeures;
            }
            return sumHours < MaxHours;
        }
    }
}
