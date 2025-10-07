using Proyecto_Aerolinea.Web.Core;
using Proyecto_Aerolinea.Web.DTOs;
using Proyecto_Aerolinea.Web.Services.Abstract;

namespace Proyecto_Aerolinea.Web.Services.Implementation
{
    public class TicketServices : ITicketServices
    {
        public Task<Response<TicketDTO>> MyCreateAsync(TicketDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<Response<TicketDTO>> MyDeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<TicketDTO>> MyGetlistAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Response<TicketDTO>> MyGetOneAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<TicketDTO>> MyUpdateAsync(TicketDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
