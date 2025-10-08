using AutoMapper;
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

        public async Task<Response<FlightDTO>> MyCreateAsync(FlightDTO dto)
        {
            return await CreateAsync<Flight, FlightDTO>(dto);
        }

        public Task<Response<object>> MyDeleteAsync(Guid id)
        {
            return DeleteAsync<Flight>(id);
        }

        public async Task<Response<FlightDTO>> UpdateAsync(FlightDTO dto)
        {
            return await EditAsync<Flight, FlightDTO>(dto, dto.Id);
        }

        public async Task<Response<FlightDTO>> MyGetOneAsync(Guid id)
        {
            return await GetOneAsync<Flight, FlightDTO>(id);
        }

        public async Task<Response<List<FlightDTO>>> MyGetListAsync()
        {
            return await GetListAsync<Flight, FlightDTO>();
        }

    }
}