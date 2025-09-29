using Proyecto_Aerolinea.Web.Core;
using Proyecto_Aerolinea.Web.DTOs;

namespace Proyecto_Aerolinea.Web.Services.Abstract
{
    public interface IAirportService
    {
        public Task<Response<AirportDTO>> CreateAsync(AirportDTO dto);
        public Task<Response<object>> DeleteAsync(AirportDTO dto);
        public Task<Response<AirportDTO>> GetById(Guid id);
        public Task<Response<AirportDTO>> UpdateAsync(AirportDTO dto);
        public Task<Response<List<AirportDTO>>> GetListAsync();
    }
}
