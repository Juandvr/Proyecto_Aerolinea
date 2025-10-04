using Proyecto_Aerolinea.Web.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Aerolinea.Web.DTOs
{
    public class FlightDTO
    {
        public Guid Id { get; set; }
        [Required, StringLength(50)]
        [Display(Name = "El codigo es necesario")]
        public required string FlightCode { get; set; }
        [Required(ErrorMessage="El campo {0} es requerido")]
        public required DateTime DepartureDateTime { get; set; }
        [Required]
        public required DateTime ArrivalDateTime { get; set; }
        [Required, StringLength(30)]
        public required string Status { get; set; }
        // FKs Airport
        [Required]
        public Guid OriginAirportId { get; set; }
        public required Airport OriginAirport { get; set; }
        [Required]
        public Guid DestinationAirportId { get; set; }
        public required Airport DestinationAirport { get; set; }
        // FK AircraftId
        [Required]
        public Guid AircraftId { get; set; }
        public required Aircraft Aircraft { get; set; }
    }
}
