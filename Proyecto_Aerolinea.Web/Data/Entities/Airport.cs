using Proyecto_Aerolinea.Web.Data.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Aerolinea.Web.Data.Entities
{
    public class Airport : IId
    {
        [Key]
        public Guid Id { get; set; }

        [Required, StringLength(80)]
        public string AirportName { get; set; }

        [Required, StringLength(80)]
        public string AirportCity { get; set; }

        [Required, StringLength(80)]
        public string AirportCountry { get; set; }

        [Required]
        public string IATACode { get; set; }
        // Relaciones con Flight
        public ICollection<Flight> OriginFlights { get; set; } = new List<Flight>();
        public ICollection<Flight> DestinationFlights { get; set; } = new List<Flight>();
    }
}