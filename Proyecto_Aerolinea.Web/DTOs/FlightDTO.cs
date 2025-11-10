using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto_Aerolinea.Web.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Aerolinea.Web.DTOs
{
    public class FlightDTO
    {
        public Guid Id { get; set; }
        [Required, StringLength(50)]
        [Display(Name = "Codigo de Avion")]
        public required string FlightCode { get; set; }
        [Required(ErrorMessage="El campo {0} es requerido")]
        [Display(Name = "Hora de partida")]
        public required DateTime DepartureDateTime { get; set; }
        [Required]
        [Display(Name = "Hora de llegada")]
        public required DateTime ArrivalDateTime { get; set; }
        [Required(ErrorMessage = "El estado del avion es necesario"), StringLength(30)]
        [Display(Name = "Estado")]
        public required string Status { get; set; }
        // FKs Airport
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Id de Aeropuerto de salida")]
        public Guid OriginAirportId { get; set; }
        public AirportDTO? OriginAirport { get; set; }
        [Required]
        [Display(Name = "Id de aeropuerto de llegada")]
        public Guid DestinationAirportId { get; set; }
        public AirportDTO? DestinationAirport { get; set; }
        // FK AircraftId
        [Required]
        [Display(Name = "Id de avion")]
        public Guid AircraftId { get; set; }
        public AircraftDTO? Aircraft { get; set; }
    }
}
