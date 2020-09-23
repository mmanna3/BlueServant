﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Core.Models;

namespace Api.Core.Repositories
{
    public interface IHabitacionRepository : IABMRepository<Habitacion>
    {
        Task<IEnumerable<Habitacion>> ListarConCamasLibres();
    }
}
