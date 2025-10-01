using Proyecto_Aerolinea.Web.Core;
using Proyecto_Aerolinea.Web.DTOs;

namespace Proyecto_Aerolinea.Web.Services.Abstract
{
    public interface IAircraftService
    {
        public Task<Response<AircraftDTO>> CreateAsync(AircraftDTO dto);
        public Task<Response<object>> DeleteAsync(Guid id);
        public Task<Response<AircraftDTO>> GetOneAsync(Guid id);
        public Task<Response<AircraftDTO>> UpdateAsync(AircraftDTO dto);
        public Task<Response<List<AircraftDTO>>> GetListAsync();
    }
}
