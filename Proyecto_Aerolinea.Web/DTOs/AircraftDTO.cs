using Proyecto_Aerolinea.Web.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Aerolinea.Web.DTOs
{
    public class AircraftDTO
    {
        [Key]
        public Guid Id { get; set; }

        [Required, StringLength(50)]
        public string? Model { get; set; }

        [Required]
        public int Capacity { get; set; }

        [Required]
        public string RegistrationNumber { get; set; }

        [Required]
        public string Airline { get; set; }

        // Relaciones
        public ICollection<Flight> Flights { get; set; } = new List<Flight>();
        public ICollection<Seat> Seats { get; set; } = new List<Seat>();
    }
}
