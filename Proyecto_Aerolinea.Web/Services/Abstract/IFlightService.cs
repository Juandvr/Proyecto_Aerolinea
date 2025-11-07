using Proyecto_Aerolinea.Web.Core;
using Proyecto_Aerolinea.Web.Core.Pagination;
using Proyecto_Aerolinea.Web.Data.Entities;
using Proyecto_Aerolinea.Web.DTOs;

namespace Proyecto_Aerolinea.Web.Services.Abstract
{
    public interface IFlightService
    {
        public Task<Response<FlightDTO>> MyCreateAsync(FlightDTO dto);
        public Task<Response<object>> MyDeleteAsync(Guid id);
        public Task<Response<FlightDTO>> MyGetOneAsync(Guid id);
        public Task<Response<FlightDTO>> UpdateAsync(FlightDTO dto);
        public Task<Response<List<FlightDTO>>> MyGetListAsync();
        public Task<Response<PaginationResponse<FlightDTO>>> MyPagination(PaginationRequest request);
    }
}
