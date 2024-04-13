using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DcxAirAPI.Domain.Flight.Models
{
    public class Journey
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public double Price { get; set; }
    }
}
