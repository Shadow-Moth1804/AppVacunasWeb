using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Vacunas.Datos.Entidades;
using Vacunas.Datos.Repositorio;
using Vacunas.Web.Models;

namespace Vacunas.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMascotaRepositorio _repositorio;

        public HomeController(IMascotaRepositorio repositorio)
        {
            _repositorio=repositorio;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> MostrarMascotas()
        {
            List<Mascota> ListaMascotas = await _repositorio.ObtenerMascota();
            return View(ListaMascotas);
        }

        public IActionResult GuardarMascota()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GuardarMascota(MascotaVM NewPet)
        {
            var respuesta = _repositorio.GuardarMascota(NewPet);
            if (respuesta)
                return RedirectToAction("MostrarMascotas");
            else
                return View();
        }

        public IActionResult EliminarMascota(int PetId)
        {
            var respuesta = _repositorio.FiltrarMascota(PetId);
            return View(respuesta);
        }

        [HttpPost]
        public IActionResult EliminarMascota(Mascota DelPet)
        {
            var respuesta = _repositorio.EliminarMascota(DelPet.PetId);
            if (respuesta)
                return RedirectToAction("MostrarMascotas");
            else
                return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}