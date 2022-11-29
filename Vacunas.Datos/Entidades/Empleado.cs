using System.ComponentModel.DataAnnotations;

namespace Vacunas.Datos.Entidades
{
    public class Empleado
    {
        public int IdEmpleado { get; set; }
        [Required(ErrorMessage = "El campo nombre es requerido")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "El campo apellido es requerido")]
        public string? LastName { get; set; }
        public string? Range { get; set; }
        public string? Email { get; set; }
        public bool Status { get; set; }
    }
}
