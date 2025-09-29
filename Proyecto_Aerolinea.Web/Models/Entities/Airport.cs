using System.ComponentModel.DataAnnotations;

namespace Proyecto_Aerolinea.Web.Models.Entities
{
    public class Airport
    {
        [Key]
<<<<<<< HEAD
        public int AirportId { get; set; }

        [Required, StringLength(80)]
        public string AirportName { get; set; }

        [Required, StringLength(80)]
        public string AirportCity { get; set; }

        [Required, StringLength(80)]
        public string AirportCountry { get; set; }

        [Required]
        public string IATACode { get; set; }

=======
        public int AirportId  { get; set; }
        [Required, StringLength(80)]
        public string AirportName { get; set; }
        [Required, StringLength(80)]
        public string AirportCity { get; set; }
        [Required, StringLength(80)]
        public string AirportCountry { get; set; }
        [Required]
        public string IATACode { get; set; }
>>>>>>> services
        // Relaciones con Flight
        public ICollection<Flight> OriginFlights { get; set; } = new List<Flight>();
        public ICollection<Flight> DestinationFlights { get; set; } = new List<Flight>();
    }
}