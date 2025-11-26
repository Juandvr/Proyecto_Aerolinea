using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Aerolinea.Web.Core;
using Proyecto_Aerolinea.Web.Core.Pagination;
using Proyecto_Aerolinea.Web.DTOs;
using Proyecto_Aerolinea.Web.Services.Abstract;
using Proyecto_Aerolinea.Web.Services.Implementation;
using System.Threading.Tasks;

namespace Proyecto_Aerolinea.Web.Controllers.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ApiController
    {
        private readonly IFlightService _flightService;
        public FlightsController(IFlightService flightService)
        {
            _flightService = flightService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            Response<List<FlightDTO>> response = await _flightService.MyGetListAsync();
            return ControllerBasicValidation(response);
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetOne([FromRoute] Guid id)
        {
            Response<FlightDTO> response = await _flightService.MyGetOneAsync(id);
            return ControllerBasicValidation(response);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FlightDTO dto)
        {
            Response<FlightDTO> response = await _flightService.MyCreateAsync(dto);
            return ControllerBasicValidation(response, ModelState);
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Edit([FromRoute] Guid id, [FromBody] FlightDTO dto)
        {
            // Asegurar que el id de la ruta y el dto coincidan (o asignar)
            dto.Id = id;
            Response<FlightDTO> response = await _flightService.UpdateAsync(dto);
            return ControllerBasicValidation(response, ModelState);
        }
        [HttpDelete("{id:guid}")]
        public async Task<ObjectResult> Delete([FromRoute]Guid id)
        {
            Response<object> response = await _flightService.MyDeleteAsync(id);
            return ControllerBasicValidation(response);
        }
    }
}
