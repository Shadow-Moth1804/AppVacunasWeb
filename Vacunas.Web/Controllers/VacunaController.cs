using Microsoft.AspNetCore.Mvc;
using Vacunas.Datos.Entidades;
using Vacunas.Datos.Repositorio;

namespace Vacunas.Web.Controllers
{
    public class VacunaController : Controller
    {
        private readonly IVacunaRepositorio _repositorio;

        public VacunaController(IVacunaRepositorio repositorio)
        {
            _repositorio=repositorio;
        }

        public async Task<IActionResult> MostrarVacunas()
        {
            List<Vacuna> listaVacuna = await _repositorio.ObtenerVacuna();
            return View(listaVacuna);
        }

        public IActionResult GuardarVacuna()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GuardarVacuna(Vacuna NewVaccine)
        {
            var respuesta = _repositorio.NuevaVacuna(NewVaccine);
            if (respuesta)
                return RedirectToAction("MostrarVacunas");
            else
                return View();
        }
    }
}