using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DcxAirAPI.Domain.Flight.Models
{
    public class Response
    {
        public bool ResponseFlag { get; set; }
        public List<List<FlightJson>> Data { get; set; }
        public string Error { get; set; }
    }
}
