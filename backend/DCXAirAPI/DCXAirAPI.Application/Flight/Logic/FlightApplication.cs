using DcxAirAPI.Domain.Flight.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DcxAirAPI.Infrastructure.Data;
using System.Net.Http.Json;

namespace DcxAirAPI.Application.Flight.Contracts
{
    public class FlightApplication : IFlightApplication
    {
        public List<FlightJson> ListFlight()
        {
            var flights = JsonFileReader.Instance.Flights;

            List<FlightJson> flightResponses = new List<FlightJson>();

            foreach (var flight in flights)
            {
                FlightJson flightResponse = new FlightJson
                {
                    Transport = flight.Transport,
                    Origin = flight.Origin,
                    Destination = flight.Destination,
                    Price = flight.Price
                };

                flightResponses.Add(flightResponse);
            }

            return flightResponses;
        }

        public async Task<Response> GetJourney(Dictionary<string, string> data)
        {
            string currency = data["currency"];
            double value = currency switch
            {
                "usd" => 1,
                "eur" => 0.91,
                _ => 4708
            };
            var response = new Response
            {
                ResponseFlag = false,
                Data = new List<List<FlightJson>>(),
                Error = ""
            };

            try
            {
                var allFlights = JsonFileReader.Instance.Flights;

                var journeys = SearchFlights(data["origin"], data["destination"], new List<FlightJson>(), new List<List<FlightJson>>(), new List<string>(), new List<string>());

                response.Data = journeys;
                response.ResponseFlag = true;

                return response;
            }
            catch (Exception error)
            {
                response.Error = error.Message;
                return response;
            }
        }

        private double CalculateTotal(List<FlightJson> flights)
        {
            return flights.Sum(flight => flight.Price);
        }

        private List<List<FlightJson>> SearchFlights(string origin, string destination, List<FlightJson> route, List<List<FlightJson>> result, List<string> flightVisited, List<string> locationVisited)
        {
            var allFlights = JsonFileReader.Instance.Flights;
            var routes = allFlights.Where(item => item.Origin == origin).ToList();

            foreach (var item in routes)
            {
                if (item.Destination == destination)
                {
                    route.Add(item);
                    result.Add(route.ToList());
                    route.Clear();
                }

                if (item.Destination != destination && !flightVisited.Contains(item.Transport.FlightNumber) && !locationVisited.Contains(item.Destination))
                {
                    var tempArr = route.ToList();
                    tempArr.Add(item);
                    var tempVisited = locationVisited.ToList();
                    tempVisited.Add(item.Origin);
                    var tempFlight = flightVisited.ToList();
                    tempFlight.Add(item.Transport.FlightNumber);
                    SearchFlights(item.Destination, destination, tempArr, result, tempFlight, tempVisited);
                }
            }
            return result;
        }
    }
}

