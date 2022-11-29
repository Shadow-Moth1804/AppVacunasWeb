using Microsoft.AspNetCore.Mvc;
using Vacunas.Datos.Repositorio;
using Vacunas.Datos.Entidades;

namespace Vacunas.Web.Controllers
{
    public class DueñoController : Controller
    {

        private readonly IDueñoRepositorio _repositorio;

        public DueñoController(IDueñoRepositorio repositorio)
        {
            _repositorio=repositorio;
        }

        public IActionResult EditarDueño(int PetId)
        {
            var oOwner = _repositorio.ObtenerDueño(PetId);
            return View(oOwner);
        }

        [HttpPost]
        public IActionResult EditarDueño(MascotaVM oOwner)
        {
            var respuesta = _repositorio.EditarDueño(oOwner);
            if (respuesta)
                return RedirectToAction("MostrarMascotas","Home");
            else
                return View();
        }
    }
}