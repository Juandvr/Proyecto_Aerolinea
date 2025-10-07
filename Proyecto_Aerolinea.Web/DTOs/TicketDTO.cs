using Proyecto_Aerolinea.Web.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Aerolinea.Web.DTOs
{
    public class TicketDTO
    {
        public required Guid Id { get; set; }

        [Required, StringLength(100)]
        public required string QRCode { get; set; }

        [Required]
        public required decimal Price { get; set; }

        // FK Reservation
        [Required]
        public required Guid ReservationId { get; set; }
        public required Reservation Reservation { get; set; }

        // FK Flight
        [Required]
        public required Guid FlightId { get; set; }
        public required Flight Flight { get; set; }

        // FK Passenger
        [Required]
        public required Guid PassengerId { get; set; }
        public required Passenger Passenger { get; set; }

        // FK SeatAssignment (uno a uno)
        public required SeatAssignment SeatAssignment { get; set; }
    }
}
