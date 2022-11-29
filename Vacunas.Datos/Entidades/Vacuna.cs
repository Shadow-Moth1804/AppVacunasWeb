namespace Vacunas.Datos.Entidades
{
    public class Vacuna
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Pathogen { get; set; }
        public bool Status { get; set; }
    }
}
