using Proyecto_Aerolinea.Web.Core;
using Proyecto_Aerolinea.Web.DTOs;
using Proyecto_Aerolinea.Web.Data.Entities;

namespace Proyecto_Aerolinea.Web.Services.Abstract
{
    public interface IFlightService
    {
        public Task<Response<FlightDTO>> MyCreateAsync(FlightDTO dto);
        public Task<Response<object>> MyDeleteAsync(Guid id);
        public Task<Response<FlightDTO>> MyGetOneAsync(Guid id);
        public Task<Response<FlightDTO>> UpdateAsync(FlightDTO dto);
        public Task<Response<List<FlightDTO>>> MyGetListAsync();
    }
}
