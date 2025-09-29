using System.ComponentModel.DataAnnotations;

namespace Proyecto_Aerolinea.Web.Models.Entities
{
    public class Flight
    {
        [Key]
        public Guid FlightId { get; set; }

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
        public int OriginAirportId { get; set; }
        public Airport OriginAirport { get; set; }

        [Required]
        public int DestinationAirportId { get; set; }
        public Airport DestinationAirport { get; set; }

        // FK Aircraft
        [Required]
        public int AircraftId { get; set; }
        public Aircraft Aircraft { get; set; }
        // Relaciones
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
        public ICollection<SeatAssignment> SeatAssignments { get; set; } = new List<SeatAssignment>();
    }
}
