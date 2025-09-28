using Proyecto_Aerolinea.Web.Core;
using Proyecto_Aerolinea.Web.Core.DTOs;
using Proyecto_Aerolinea.Web.Models.Entities;

namespace Proyecto_Aerolinea.Web.Services.Abstract
{
    public interface IFlightService
    {
        public Task<Response<List<FlightDTO>>> Getlistasync();
    }
}
