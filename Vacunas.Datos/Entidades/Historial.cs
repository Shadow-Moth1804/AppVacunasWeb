namespace Vacunas.Datos.Entidades
{
    public class Historial
    {
        public int HId { get; set; }
        public string? PetName { get; set; }
        public string? Vaccine { get; set; }
        public decimal Weight { get; set; }
        public DateTime Date { get; set; }
        public string? Satage { get; set; }
        public string? NameDoctor { get; set; }
        public bool Status { get; set; }
        public int IdPet { get; set; }
        public int IdVacc { get; set; }
        public int DocId { get; set; }
    }
}
