using AutoMapper;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using Proyecto_Aerolinea.Web.Core;
using Proyecto_Aerolinea.Web.Data;
using Proyecto_Aerolinea.Web.Data.Entities;
using Proyecto_Aerolinea.Web.DTOs;
using Proyecto_Aerolinea.Web.Services.Abstract;

namespace Proyecto_Aerolinea.Web.Services.Implementation
{
    public class AirportService : IAirportService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public AirportService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Response<AirportDTO>> CreateAsync(AirportDTO dto)
        {
            try
            {
                Airport airport = _mapper.Map<Airport>(dto);

                Guid id = Guid.NewGuid();
                airport.AirportId = id;

                await _context.AddAsync(airport);
                await _context.SaveChangesAsync();

                dto.AirportId = id;
                return Response<AirportDTO>.Success(dto, "Aeropuerto creado con éxito");
            }
            catch (Exception ex)
            {
                return Response<AirportDTO>.Failure(ex);
            }
        }

        public Task<Response<object>> DeleteAsync(AirportDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<Response<AirportDTO>> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<List<AirportDTO>>> GetListAsync()
        {
            try
            {
                List<Airport> airports = await _context.Airports.ToListAsync();

                List<AirportDTO> list = _mapper.Map<List<AirportDTO>>(airports);

                return Response<List<AirportDTO>>.Success(list);
            }
            catch (Exception ex)
            {
                return Response<List<AirportDTO>>.Failure(ex);
            }
        }

        public Task<Response<AirportDTO>> UpdateAsync(AirportDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
