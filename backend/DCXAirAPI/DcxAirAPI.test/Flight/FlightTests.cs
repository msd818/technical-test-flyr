using DcxAirAPI.Application.Flight.Contracts;
using DcxAirAPI.Domain.Flight.Models;
using NUnit.Framework;

namespace DcxAirAPI.test.Flight
{
    [TestFixture]
    public class FlightTests
    {
        [Test]
        public void ListFlight_ReturnsListOfFlights()
        {
            var flightApplication = new FlightApplication();

            List<FlightJson> flights = flightApplication.ListFlight();

            Assert.That(flights, Is.Not.Null);
            Assert.That(flights, Is.InstanceOf<List<FlightJson>>());
        }

        [Test]
        public void GetJourney_WhenCalled_ReturnsResponse()
        {
            var flightApplication = new FlightApplication();
            var data = new Dictionary<string, string>
            {
                { "currency", "usd" },
                { "origin", "OriginAirportCode" },
                { "destination", "DestinationAirportCode" },
                { "typeJourney", "oneway" }
            };

            var responseTask = flightApplication.GetJourney(data);
            responseTask.Wait();
            var response = responseTask.Result;

            Assert.That(response.ResponseFlag, Is.True);
            Assert.That(response.Data, Is.Not.Null);
            Assert.That(response.Error, Is.Empty);
        }
    }
}

