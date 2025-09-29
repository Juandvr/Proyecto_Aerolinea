using Microsoft.EntityFrameworkCore;
using Proyecto_Aerolinea.Web.Core;
using Proyecto_Aerolinea.Web.Data;
using Proyecto_Aerolinea.Web.Data.Entities;
using Proyecto_Aerolinea.Web.DTOs;
using Proyecto_Aerolinea.Web.Services.Abstract;

namespace Proyecto_Aerolinea.Web.Services.Implementation
{
    public class FlightService : IFlightService
    {
        private readonly DataContext _context;
        public FlightService(DataContext context)
        {
            _context=context;
        }

        public Task<Response<FlightDTO>> CreateAsync(FlightDTO dto)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<List<FlightDTO>>> Getlistasync()
        {
            Response<List<FlightDTO>> response = new Response<List<FlightDTO>>();
            try
            {
                List<Flight> flights = await _context.Flights.ToListAsync();
                List<FlightDTO> flightDTOs = new List<FlightDTO>();

                flightDTOs = flights.Select(f => new FlightDTO
                {
                    FlightId=f.FlightId,
                    FlightCode=f.FlightCode,
                    Status=f.Status,
                    ArrivalDateTime=f.ArrivalDateTime,
                    AircraftId=f.AircraftId,
                    Aircraft=f.Aircraft,
                    DepartureDateTime =f.DepartureDateTime,
                    OriginAirport=f.OriginAirport,
                    OriginAirportId=f.OriginAirportId,
                    DestinationAirportId = f.DestinationAirportId,
                    DestinationAirport =f.DestinationAirport,
                }).ToList();
                response.Succeed = true;
                response.Result = flightDTOs;

                return response;
            }
            catch (Exception ex)
            {
                return new Response<List<FlightDTO>>
                {
                    Succeed = false,
                    Message = "No se pudo cargar la lista de vuelos",
                    Errors = new List<string> { ex.Message }
                };
            }
        }
    }
}
