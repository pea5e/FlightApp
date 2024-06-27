using FlightApp.Core.Models;

namespace FlightApp.API
{
    public interface IFDPStrategy
    {
        public bool CalculateFDP(List<Flight> flights);
    }
}
