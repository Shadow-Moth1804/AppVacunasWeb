﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vacunas.Datos.Entidades;

namespace Vacunas.Datos.Repositorio
{
    public interface IVacunaRepositorio
    {
        bool NuevaVacuna(Vacuna NuevaVacuna);
        Task<List<Vacuna>> ObtenerVacuna();
    }
}
