using Proyecto_Aerolinea.Web.Core;
using Proyecto_Aerolinea.Web.DTOs;
using Proyecto_Aerolinea.Web.Data.Entities;

namespace Proyecto_Aerolinea.Web.Services.Abstract
{
    public interface IFlightService
    {
        public Task<Response<FlightDTO>> CreateAsync(FlightDTO dto);
        public Task<Response<List<FlightDTO>>> Getlistasync();
    }
}
