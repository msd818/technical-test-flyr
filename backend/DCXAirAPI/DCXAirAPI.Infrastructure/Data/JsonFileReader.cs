using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using DcxAirAPI.Domain.Flight.Models;
using Newtonsoft.Json;

namespace DcxAirAPI.Infrastructure.Data
{
    public class JsonFileReader
    {
        private static JsonFileReader _instance;
        private static readonly object _lock = new object();
        private List<FlightJson> _flights;

        private JsonFileReader()
        {
            _flights = ReadFlightsFromFile("./JSON/markets.json");

        }

        public static JsonFileReader Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new JsonFileReader();
                }
                return _instance;
            }
        }

        public List<FlightJson> Flights => _flights;

        public List<FlightJson> ReadFlightsFromFile(string filePath)
        {
            string jsonString = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<FlightJson>>(jsonString);
        }
    }
}
