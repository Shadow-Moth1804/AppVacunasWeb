using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vacunas.Datos.Entidades;

namespace Vacunas.Datos.Repositorio
{
    public interface IHistorialRepositorio
    {
        Historial FiltrarMascota(int IdMascota);
        Vacuna FiltrarVacuna(int idVacuna);
        bool NuevoRegistro(Historial historial);
        Task<List<Historial>> ObtenerHistorial();
    }
}
