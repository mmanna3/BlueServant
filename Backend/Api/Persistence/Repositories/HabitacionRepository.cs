using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Core.Entidades;
using Api.Core.Enums;
using Api.Core.Repositories;
using Api.Persistence.Config;
using Microsoft.EntityFrameworkCore;

namespace Api.Persistence.Repositories
{
    public class HabitacionRepository : ABMRepository<Habitacion>, IHabitacionRepository
    {
        public HabitacionRepository(AppDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Habitacion>> Listar()
        {
            return await _context.Habitaciones
                                    .Include(x => x.CamasIndividuales)
                                    .Include(x => x.CamasCuchetas)
                                        .ThenInclude(x => x.Abajo)
                                    .Include(x => x.CamasCuchetas)
                                        .ThenInclude(x => x.Arriba)
                                    .Include(x => x.CamasMatrimoniales)
                                    .ToListAsync();
        }

        public override async Task<Habitacion> ObtenerPorId(int id)
        {
            return await _context.Habitaciones
                .Include(x => x.CamasIndividuales)
                .Include(x => x.CamasCuchetas)
                    .ThenInclude(x => x.Abajo)
                .Include(x => x.CamasCuchetas)
                    .ThenInclude(x => x.Arriba)
                .Include(x => x.CamasMatrimoniales)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Habitacion>> ListarConCamasLibresEntre(DateTime primeraNoche, DateTime ultimaNoche)
        {
	        var habitacionesCompartidasConCamasLibres = await HabitacionesCompartidasConCamasLibresEntre(primeraNoche, ultimaNoche);
	        var habitacionesPrivadasLibres = await HabitacionesPrivadasLibresEntre(primeraNoche, ultimaNoche).ToListAsync();

			return habitacionesCompartidasConCamasLibres.Concat(habitacionesPrivadasLibres.Cast<Habitacion>());
        }

        private IQueryable<HabitacionPrivada> HabitacionesPrivadasLibresEntre(DateTime primeraNoche, DateTime ultimaNoche)
        {
	        var idsDeHabitacionesOcupadasAlMenosUnaNocheEnElRango =
		        _context.ReservaHabitacionesPrivadas
					.Where(rc => rc.Reserva.Estado != ReservaEstadoEnum.Cancelada)
			        .Where(rc => (rc.Reserva.PrimeraNoche <= primeraNoche && rc.Reserva.UltimaNoche >= primeraNoche)
			                     || (rc.Reserva.PrimeraNoche <= ultimaNoche && rc.Reserva.UltimaNoche >= primeraNoche))
			        .Select(c => c.HabitacionPrivada.Id)
			        .ToList();

	        var habitacionesPrivadasLibres = _context.HabitacionesPrivadas
				.Where(x => x.EstaHabilitada)
				.Include(x => x.CamasIndividuales)
				.Include(x => x.CamasMatrimoniales)
				.Include(x => x.CamasCuchetas)
					.ThenInclude(x => x.Abajo)
				.Include(x => x.CamasCuchetas)
					.ThenInclude(x => x.Arriba)
				.Select(x => 
					!idsDeHabitacionesOcupadasAlMenosUnaNocheEnElRango.Contains(x.Id) ? 
					x : 
					new HabitacionPrivada
					{
						Id = x.Id,
						Nombre = x.Nombre,
						CamasIndividuales = new List<CamaIndividual>(),
						CamasCuchetas = new List<CamaCucheta>(),
						CamasMatrimoniales = new List<CamaMatrimonial>(),
					});

	        return habitacionesPrivadasLibres;
        }

		private async Task<IEnumerable<HabitacionCompartida>> HabitacionesCompartidasConCamasLibresEntre(DateTime primeraNoche, DateTime ultimaNoche)
        {
	        var idsDeCamasOcupadasAlMenosUnaNocheEnElRango = await _context.ReservaCamas
		        .Where(rc => rc.Reserva.Estado != ReservaEstadoEnum.Cancelada)
		        .Where(rc => (rc.Reserva.PrimeraNoche <= primeraNoche && rc.Reserva.UltimaNoche >= primeraNoche)
		                     || (rc.Reserva.PrimeraNoche <= ultimaNoche && rc.Reserva.UltimaNoche >= primeraNoche))
		        .Select(c => c.Cama.Id)
		        .ToListAsync();

	        var habitaciones = await _context.HabitacionesCompartidas
		        .Where(x => x.EstaHabilitada)
		        .Include(x => x.CamasIndividuales)
		        .Include(x => x.CamasMatrimoniales)
		        .Include(x => x.CamasCuchetas)
		            .ThenInclude(x => x.Abajo)
		        .Include(x => x.CamasCuchetas)
		            .ThenInclude(x => x.Arriba)
		        .ToListAsync();

	        return habitaciones.Select(x =>
		        new HabitacionCompartida
		        {
			        Id = x.Id,
			        Nombre = x.Nombre,
			        CamasIndividuales = x.CamasIndividuales.Where(c => c.EstaHabilitada && !idsDeCamasOcupadasAlMenosUnaNocheEnElRango.Contains(c.Id)).ToList(),
			        CamasMatrimoniales = x.CamasMatrimoniales.Where(c => c.EstaHabilitada && !idsDeCamasOcupadasAlMenosUnaNocheEnElRango.Contains(c.Id)).ToList(),
			        CamasCuchetas = x.CamasCuchetas.Select(cc => new CamaCucheta
			        {
				        Id = cc.Id,
				        Abajo = cc.Abajo != null && cc.Abajo.EstaHabilitada && !idsDeCamasOcupadasAlMenosUnaNocheEnElRango.Contains(cc.Abajo.Id) ? cc.Abajo : null,
				        Arriba = cc.Arriba != null && cc.Arriba.EstaHabilitada && !idsDeCamasOcupadasAlMenosUnaNocheEnElRango.Contains(cc.Arriba.Id) ? cc.Arriba : null,
				        Habitacion = x
			        }).ToList()
		        });
        }
    }
}
