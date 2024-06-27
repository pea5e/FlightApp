using FlightApp.Core.Models;

namespace FlightApp.API
{
    public class IncludeWOCL : IFDPStrategy
    {
        private double MaxHours = 13;
        public bool CalculateFDP(List<Flight> flights)
        {
            MaxHours -= (flights.Count - 1) * 0.5;
            MaxHours -= Math.Min((6 - flights.Last().Arrive.Hour) - ( flights.Last().Arrive.Minute / 60) ,2);
            double sumHours = 0;
            foreach (Flight flight in flights)
            {
                sumHours += flight.TotalHeures;
            }
            return sumHours < MaxHours;
        }
    }
}
