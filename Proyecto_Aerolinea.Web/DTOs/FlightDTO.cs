using Proyecto_Aerolinea.Web.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Aerolinea.Web.DTOs
{
    public class FlightDTO
    {
        public Guid Id { get; set; }
        [Required, StringLength(50)]
        public string FlightCode { get; set; }
        [Required]
        public DateTime DepartureDateTime { get; set; }
        [Required]
        public DateTime ArrivalDateTime { get; set; }
        [Required, StringLength(30)]
        public string Status { get; set; }
        // FKs Airport
        [Required]
        public Guid OriginAirportId { get; set; }
        public Airport OriginAirport { get; set; }
        [Required]
        public Guid DestinationAirportId { get; set; }
        public Airport DestinationAirport { get; set; }
        // FK AircraftId
        [Required]
        public Guid AircraftId { get; set; }
        public Aircraft Aircraft { get; set; }
    }
}
