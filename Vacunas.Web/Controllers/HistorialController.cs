using Microsoft.AspNetCore.Mvc;
using Vacunas.Datos.Entidades;
using Vacunas.Datos.Repositorio;

namespace Vacunas.Web.Controllers
{
    public class HistorialController : Controller
    {
        private readonly IHistorialRepositorio _repositorio;

        public HistorialController(IHistorialRepositorio repositorio)
        {
            _repositorio=repositorio;
        }

        public async Task<IActionResult> ObtenerHistorial()
        {
            List<Historial> lista = await _repositorio.ObtenerHistorial();
            return View(lista);
        }

        public IActionResult GuardarRegistro(int PetId)
        {
            var respuesta = _repositorio.FiltrarMascota(PetId);
            return View(respuesta);
        }

        [HttpPost]
        public IActionResult GuardarRegistro(Historial nuevoregistro)
        {
            var respuesta = _repositorio.NuevoRegistro(nuevoregistro);
            if (respuesta)
                return RedirectToAction("ObtenerHistorial");
            else
                return View();
        }
    }
}
