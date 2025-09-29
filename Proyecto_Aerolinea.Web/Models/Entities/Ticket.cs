using System.ComponentModel.DataAnnotations;

namespace Proyecto_Aerolinea.Web.Models.Entities
{
    public class Ticket
    {
        [Key]
        public Guid TicketId { get; set; }

        [Required, StringLength(100)]
        public string QRCode { get; set; }

        [Required]
        public decimal Price { get; set; }

        // FK Reservation
        [Required]
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }

        // FK Flight
        [Required]
        public int FlightId { get; set; }
        public Flight Flight { get; set; }

        // FK Passenger
        [Required]
        public int PassengerId { get; set; }
        public Passenger Passenger { get; set; }

        // FK SeatAssignment (uno a uno)
        public SeatAssignment SeatAssignment { get; set; }
    }
}
