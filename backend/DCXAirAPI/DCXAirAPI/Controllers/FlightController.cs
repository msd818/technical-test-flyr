using DcxAirAPI.Application.Flight.Contracts;
using DcxAirAPI.Domain.Common.DTOs;
using DcxAirAPI.Domain.Flight.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DcxAirAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlightController : ControllerBase
    {
        private readonly IFlightApplication _flightApplication;

        public FlightController(IFlightApplication deliveryBusiness)
        {
            _flightApplication = deliveryBusiness;
        }


        [HttpGet("listFlight")]
        public async Task<ActionResult<ResponseDTO>> ListFlight()
        {
            var flights = _flightApplication.ListFlight();

            if (flights != null && flights.Any())
            {
                return Ok(new ResponseDTO
                {
                    Data = flights,
                    Message = "Success",
                    StatusCode = (int)HttpStatusCode.OK
                });
            }

            return NotFound();
        }

        [HttpGet("searchFlights")]
        public async Task<ActionResult<ResponseDTO>> SearchFlights(string origin, string destination, string currency)
        {
            if (string.IsNullOrEmpty(origin) || string.IsNullOrEmpty(destination))
            {
                return BadRequest("Origin and destination are required.");
            }

            var data = new Dictionary<string, string>
            {
                { "origin", origin },
                { "destination", destination },
                { "currency", currency },
            };

            var response = await _flightApplication.GetJourney(data);

            if (response.ResponseFlag)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response.Error);
            }
        }
    }
}
