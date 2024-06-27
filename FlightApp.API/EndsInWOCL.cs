using FlightApp.Core.Models;

namespace FlightApp.API
{
    public class EndsInWOCL : IFDPStrategy
    {
        private double MaxHours = 13;
        public bool CalculateFDP(List<Flight> flights)
        {
            MaxHours -= (flights.Count - 1) * 0.5;
            MaxHours -= (Math.Min((flights.Last().Arrive.Hour - 2) + flights.Last().Arrive.Minute / 60, 4))/2;
            double sumHours = 0;
            foreach (Flight flight in flights)
            {
                sumHours += flight.TotalHeures;
            }
            return sumHours < MaxHours;
        }
    }
}
