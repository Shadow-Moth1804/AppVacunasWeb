using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vacunas.Datos.Entidades;

namespace Vacunas.Datos.Repositorio
{
    public interface IMascotaRepositorio
    {
        bool EliminarMascota(int IdMascota);
        Mascota FiltrarMascota(int IdMascota);
        bool GuardarMascota(MascotaVM NuevaMascota);
        Task<List<Mascota>> ObtenerMascota();
    }
}
