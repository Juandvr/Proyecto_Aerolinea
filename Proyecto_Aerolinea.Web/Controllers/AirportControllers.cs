using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Proyecto_Aerolinea.Web.Models.Entities;
using Proyecto_Aerolinea.Web.Services.AirportServices;
using SystemStore.Services.AirportServices;

namespace SystemStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AirportController : ControllerBase
    {

        // Constructor Controllers

        private readonly AddAirport _addAirport;
        private readonly UpdateAirport _updateAirport;
        private readonly GetAirportById _getAirportById;
        private readonly DeleteAirport _deleteAirport;

        public AirportController(AddAirport addAirport, UpdateAirport updateAirport, GetAirportById getAirportById, DeleteAirport deleteAirport)
        {
            _addAirport = addAirport;
            _updateAirport = updateAirport;
            _getAirportById = getAirportById;
            _deleteAirport = deleteAirport;
            _deleteAirport = deleteAirport;
        }

        // Add product

        [HttpPost]
        public async Task<IActionResult> CreateAirport([FromBody] AirportDto dto)
        {
            try
            {
                var airport = await _addAirport.Execute(dto);
                return Ok(airport);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Error real
            }
        }

        // Update product
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAirport(Guid id, [FromBody] AirportDto dto)
        {
            try
            {
                var updatedAirport = await _updateAirport.Execute(id, dto);
                if (updatedAirport == null) return NotFound("Airport no encontrado");
                return Ok(updatedAirport);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Muestra el error real
            }
        }

        // GetAirportById

        [HttpGet("{id}")]
        public async Task<ActionResult<Airport>> GetByAirportId(Guid id)
        {
            var airport = await _getAirportById.Execute(id);

            if (airport == null)
                return NotFound(new { message = "Airport no encontrado" });

            return Ok(airport);
        }

        // Delete Airport
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAirport(Guid id)
        {
            var deleted = await _deleteAirport.Execute(id);
            if (!deleted) return NotFound(new { message = "Airport no encontrado" });

            return NoContent();
        }
    }
}