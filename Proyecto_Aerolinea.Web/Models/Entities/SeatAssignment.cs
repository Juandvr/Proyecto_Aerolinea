using System.ComponentModel.DataAnnotations;

namespace Proyecto_Aerolinea.Web.Models.Entities
{
    public class SeatAssignment
    {
        [Key]
        public int SeatAssignmentId { get; set; }
        // FK Flight
        [Required]
        public int FlightId { get; set; }
        public Flight Flight { get; set; }
        // FK Ticket
        [Required]
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }
        // FK AircraftId
        [Required]
        public int AircraftId { get; set; }
        public Aircraft Aircraft { get; set; }
        // Relacion con Ticket
        public ICollection<Ticket> Tickets { get; set; }
    }
}
