using System.ComponentModel.DataAnnotations;

namespace Vacunas.Datos.Entidades
{
    public class Mascota
    {
        public int PetId { get; set; }
        [Required(ErrorMessage = "El campo nombre es necesario")]
        public string? PetName { get; set; }
        public string? OwnerName { get; set; }
        public string? Breed { get; set; }
        public string? Gender { get; set; }
        public DateTime? Dateb { get; set; }
        public string? AddData { get; set; }
        public bool Status { get; set; }
    }
}
