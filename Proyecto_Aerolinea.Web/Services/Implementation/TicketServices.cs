using Proyecto_Aerolinea.Web.Core;
using Proyecto_Aerolinea.Web.Data.Entities;
using Proyecto_Aerolinea.Web.DTOs;
using Proyecto_Aerolinea.Web.Services.Abstract;
using Proyecto_Aerolinea.Web.Services;
using Proyecto_Aerolinea.Web.Data;
using AutoMapper;

namespace Proyecto_Aerolinea.Web.Services.Implementation
{
    public class TicketServices : CustomQueryableOperationsService, ITicketServices
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public TicketServices(DataContext context, IMapper mapper) : base(context, mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<Response<TicketDTO>> MyCreateAsync(TicketDTO dto)
        {
            return await CreateAsync<Ticket, TicketDTO>(dto);
        }

        public async Task<Response<object>> MyDeleteAsync(Guid id)
        {
            return await DeleteAsync<Ticket>(id);
        }

        public async Task<Response<List<TicketDTO>>> MyGetlistAsync()
        {
            return await GetListAsync<Ticket, TicketDTO>();
        }

        public async Task<Response<TicketDTO>> MyGetOneAsync(Guid id)
        {
            return await GetOneAsync<Ticket, TicketDTO>(id);
        }

        public async Task<Response<TicketDTO>> MyUpdateAsync(TicketDTO dto)
        {
            return await EditAsync<Ticket, TicketDTO>(dto, dto.Id);
        }
    }
}
