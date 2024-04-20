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

        public FlightController(IFlightApplication flightApplication)
        {
            _flightApplication = flightApplication;
        }

        [HttpGet("listFlight")]
        public async Task<ActionResult<ResponseDTO>> ListFlight()
        {
            try
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
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new ErrorResponseDTO<ErrorDTO>
                {
                    Errors = new ErrorDTO
                    {
                        Message = ex.Message,
                        Code = (int)HttpStatusCode.InternalServerError
                    }
                });
            }
        }

        [HttpGet("searchFlights")]
        public async Task<ActionResult<ResponseDTO>> SearchFlights(string origin, string destination, string currency, string typeJourney)
        {
            try
            {
                if (string.IsNullOrEmpty(origin) || string.IsNullOrEmpty(destination))
                {
                    return BadRequest(new ErrorResponseDTO<ErrorDTO>
                    {
                        Errors = new ErrorDTO
                        {
                            Message = "Origin and destination are required.",
                            Code = (int)HttpStatusCode.BadRequest
                        }
                    });
                }

                var data = new Dictionary<string, string>
            {
                { "origin", origin },
                { "destination", destination },
                { "currency", currency },
                { "typeJourney", typeJourney },
            };

                var response = await _flightApplication.GetJourney(data);

                if (response.ResponseFlag)
                {
                    return Ok(new ResponseDTO<Response>
                    {
                        Data = response,
                        Message = "Success",
                        StatusCode = (int)HttpStatusCode.OK
                    });
                }
                else
                {
                    return BadRequest(new ErrorResponseDTO<ErrorDTO>
                    {
                        Message = "Error",
                        StatusCode = (int)HttpStatusCode.BadRequest
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new ErrorResponseDTO<ErrorDTO>
                {
                    Errors = new ErrorDTO
                    {
                        Message = ex.Message,
                        Code = (int)HttpStatusCode.InternalServerError
                    }
                });
            }
        }
    }

}
