﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Core.Models;
using Api.Core.Repositories;
using Api.Persistence.Config;
using Microsoft.EntityFrameworkCore;

namespace Api.Persistence.Repositories
{
    public class ReservaRepository : ABMRepository<Reserva>, IReservaRepository
    {
        public ReservaRepository(AppDbContext context) : base(context)
        {
        }
        public override async Task<IEnumerable<Reserva>> Listar()
        {
            return await _context.Reservas
                .Include(x => x.ReservaCamas)
                .ToListAsync();
        }

        public async Task<IEnumerable<Reserva>> ListarMensuales(int anio, int mes)
        {
            return await _context.Reservas
                .Include(x => x.ReservaCamas)
                .ThenInclude(x => x.Cama)
                .Where(x => x.Desde <= new DateTime(anio, mes, DateTime.DaysInMonth(anio, mes)) && x.Hasta >= new DateTime(anio, mes, 1))
                .ToListAsync();
        }

        public async Task<IEnumerable<Reserva>> ListarActuales()
        {
            return await _context.Reservas
                .Include(x => x.ReservaCamas)
                .ThenInclude(x => x.Cama)
                .Where(x => x.Desde <= DateTime.Today.AddDays(15) && x.Hasta >= DateTime.Today.AddDays(-1))
                .ToListAsync();
        }

        public async Task<IEnumerable<Reserva>> ListarCheckoutsDeHoy()
        {
            return await _context.Reservas
                .Include(x => x.ReservaCamas)
                .ThenInclude(x => x.Cama)
                .Where(x => x.Hasta == DateTime.Today.AddDays(-1))
                .ToListAsync();
        }
    }
}
