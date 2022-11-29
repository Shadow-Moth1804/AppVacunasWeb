using Microsoft.AspNetCore.Mvc;
using Vacunas.Datos.Entidades;
using Vacunas.Datos.Repositorio;

namespace Vacunas.Web.Controllers
{
    public class EmpleadoController : Controller
    {
        private readonly IEmpleadoRepositorio _repositorio;

        public EmpleadoController(IEmpleadoRepositorio repositorio)
        {
            _repositorio=repositorio;
        }

        public async Task<IActionResult> MostrarEmpleados()
        {
            List<Empleado> listaEmpleado = await _repositorio.ObtenerEmpleado();
            return View(listaEmpleado);
        }

        public IActionResult GuardarEmpleado()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GuardarEmpleado(Empleado NewEmployee)
        {
            var respuesta = _repositorio.NuevoEmpleado(NewEmployee);
            if (respuesta)
                return RedirectToAction("MostrarEmpleados");
            else
                return View();
        }
    }
}
