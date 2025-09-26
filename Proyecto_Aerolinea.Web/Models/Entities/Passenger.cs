using System.ComponentModel.DataAnnotations;

namespace Proyecto_Aerolinea.Web.Models.Entities
{
    public class Passenger
    {
        [Key]
        public int PassengerId { get; set; }
        [Required, StringLength(50)]
        public string FirsName { get; set; }
        [Required, StringLength(50)]
        public string LastName  { get; set; }
        [Required, StringLength(20)]
        public string Phone { get; set; }
        [Required, StringLength(100)]
        public string Email { get; set; }   
        [Required]
        public DateTime BirthDate { get; set; }

        // Relacion con Ticket
        public ICollection<Ticket> Tickets { get; set; }
    }
}
