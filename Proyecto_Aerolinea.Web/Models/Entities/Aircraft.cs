using System.ComponentModel.DataAnnotations;

namespace Proyecto_Aerolinea.Web.Models.Entities
{
    public class Aircraft
    {
        [Key]
        public Guid AircraftId { get; set; }

        [Required, StringLength(50)]
        public string Model { get; set; }

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
