using System.ComponentModel.DataAnnotations;

namespace Vacunas.Datos.Entidades
{
    public class MascotaVM
    {
        public int OwnerId { get; set; }
        [Required(ErrorMessage = "El campo nombre es requerido")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "El campo apellido es requerido")]
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? ClientAdData { get; set; }
        [Required(ErrorMessage = "El campo nombre es necesario")]
        public string? PetName { get; set; }
        public string? OwnerName { get; set; }
        public string? Breed { get; set; }
        public string? Gender { get; set; }
        public DateTime? Dateb { get; set; }
        public string? PetAddData { get; set; }
    }
}
