using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto_Aerolinea.Web.Core;
using Proyecto_Aerolinea.Web.Data.Entities;
using Proyecto_Aerolinea.Web.DTOs;

namespace Proyecto_Aerolinea.Web.Services.Abstract
{
    public interface IFlightService
    {
        public Task<Response<FlightDTO>> CreateAsync(FlightDTO dto);
        public Task<Response<object>> DeleteAsync(Guid id);
        public Task<Response<FlightDTO>> GetOneAsync(Guid id);
        public Task<Response<FlightDTO>> UpdateAsync(FlightDTO dto);
        public Task<Response<List<FlightDTO>>> GetListAsync();
    }
}
