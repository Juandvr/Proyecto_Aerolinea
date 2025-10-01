using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PrivateBlog.Web.Services;
using Proyecto_Aerolinea.Web.Core;
using Proyecto_Aerolinea.Web.Data;
using Proyecto_Aerolinea.Web.Data.Entities;
using Proyecto_Aerolinea.Web.DTOs;
using Proyecto_Aerolinea.Web.Services.Abstract;

namespace Proyecto_Aerolinea.Web.Services.Implementation
{
    public class FlightService : CustomQueryableOperationsService, IFlightService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public FlightService(DataContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<FlightDTO>> CreateAsync(FlightDTO dto)
        {
            return await CreateAsync<Flight, FlightDTO>(dto);
        }

        public Task<Response<object>> DeleteAsync(Guid id)
        {
            return DeleteAsync<Flight>(id);
        }

        public async Task<Response<FlightDTO>> UpdateAsync(FlightDTO dto)
        {
            return await EditAsync<Flight, FlightDTO>(dto, dto.Id);
        }

        public async Task<Response<FlightDTO>> GetOneAsync(Guid id)
        {
            return await GetOneAsync<Flight, FlightDTO>(id);
        }

        public async Task<Response<List<FlightDTO>>> GetListAsync()
        {
            List<Flight> flights = await _context.Flights
            .Include(f => f.OriginAirport)
            .Include(f => f.DestinationAirport)
            .Include(f => f.Aircraft)
            .ToListAsync();

            List<FlightDTO> dtos = _mapper.Map<List<FlightDTO>>(flights);

            return Response<List<FlightDTO>>.Success(dtos);
        }
    }
}