using Proyecto_Aerolinea.Web.Core;
using Proyecto_Aerolinea.Web.DTOs;
using System.Threading.Tasks;

namespace Proyecto_Aerolinea.Web.Services.Abstract
{
    public interface ITicketServices
    {
        public Task<Response<TicketDTO>> MyCreateAsync(TicketDTO dto);
        public Task<Response<TicketDTO>> MyGetlistAsync();
        public Task<Response<TicketDTO>> MyGetOneAsync(Guid id);
        public Task<Response<TicketDTO>> MyUpdateAsync(TicketDTO dto);
        public Task<Response<TicketDTO>> MyDeleteAsync(Guid id);
    }
}
