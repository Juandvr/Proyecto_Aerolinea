using AutoMapper;
using Proyecto_Aerolinea.Web.Core;
using Proyecto_Aerolinea.Web.Data;
using Proyecto_Aerolinea.Web.Data.Entities;
using Proyecto_Aerolinea.Web.DTOs;
using Proyecto_Aerolinea.Web.Services.Abstract;

namespace Proyecto_Aerolinea.Web.Services.Implementation
{
    public class AirportService : CustomQueryableOperationsService, IAirportService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public AirportService(DataContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<AirportDTO>> CreateAsync(AirportDTO dto)
        {
            return await CreateAsync<Airport, AirportDTO>(dto);
        }

        public Task<Response<object>> DeleteAsync(Guid id)
        {
            return DeleteAsync<Airport>(id);
        }

        public async Task<Response<AirportDTO>> UpdateAsync(AirportDTO dto)
        {
            return await EditAsync<Airport, AirportDTO>(dto, dto.Id);
        }

        public async Task<Response<AirportDTO>> GetOneAsync(Guid id)
        {
            return await GetOneAsync<Airport, AirportDTO>(id);
        }

        public async Task<Response<List<AirportDTO>>> GetListAsync()
        {
            return await GetListAsync<Airport, AirportDTO>();
        }

    }
}
