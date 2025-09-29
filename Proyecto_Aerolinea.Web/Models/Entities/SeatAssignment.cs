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

        // FK Ticket (uno a uno)
        [Required]
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }

        // FK Seat (opcional)
        public int? SeatId { get; set; }
        public Seat Seat { get; set; }
    }
}
