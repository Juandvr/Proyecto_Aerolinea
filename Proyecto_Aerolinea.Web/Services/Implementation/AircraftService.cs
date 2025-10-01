using AutoMapper;
using PrivateBlog.Web.Services;
using Proyecto_Aerolinea.Web.Core;
using Proyecto_Aerolinea.Web.Data;
using Proyecto_Aerolinea.Web.Data.Entities;
using Proyecto_Aerolinea.Web.DTOs;
using Proyecto_Aerolinea.Web.Services.Abstract;

namespace Proyecto_Aerolinea.Web.Services.Implementation
{
    public class AircraftService : CustomQueryableOperationsService, IAircraftService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public AircraftService(DataContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<AircraftDTO>> CreateAsync(AircraftDTO dto)
        {
            return await CreateAsync<Aircraft, AircraftDTO>(dto);
        }

        public Task<Response<object>> DeleteAsync(Guid id)
        {
            return DeleteAsync<Aircraft>(id);
        }

        public async Task<Response<AircraftDTO>> UpdateAsync(AircraftDTO dto)
        {
            return await EditAsync<Aircraft, AircraftDTO>(dto, dto.Id);
        }

        public async Task<Response<AircraftDTO>> GetOneAsync(Guid id)
        {
            return await GetOneAsync<Aircraft, AircraftDTO>(id);
        }

        public async Task<Response<List<AircraftDTO>>> GetListAsync()
        {
            return await GetListAsync<Aircraft, AircraftDTO>();
        }
    }
}
