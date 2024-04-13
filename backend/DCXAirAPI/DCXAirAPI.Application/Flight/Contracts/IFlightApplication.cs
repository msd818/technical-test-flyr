using DcxAirAPI.Domain.Flight.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DcxAirAPI.Application.Flight.Contracts
{
    public interface IFlightApplication
    {
        List<FlightJson> ListFlight();

        Task<Response> GetJourney(Dictionary<string, string> data);
    }
}
