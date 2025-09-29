using System.ComponentModel.DataAnnotations;

namespace Proyecto_Aerolinea.Web.Data.Entities
{
    public class SeatAssignment
    {
        [Key]
        public Guid SeatAssignmentId { get; set; }

        // FK Flight
        [Required]
        public Guid FlightId { get; set; }
        public Flight Flight { get; set; }

        // FK Ticket (uno a uno)
        [Required]
        public Guid TicketId { get; set; }
        public Ticket Ticket { get; set; }

        // FK Seat (opcional)
        public Guid? SeatId { get; set; }
        public Seat Seat { get; set; }
    }
}
