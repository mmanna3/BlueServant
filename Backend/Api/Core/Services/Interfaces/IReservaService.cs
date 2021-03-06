﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Core.Entidades;
using Api.Core.Enums;

namespace Api.Core.Services.Interfaces
{
    public interface IReservaService
    {
	    Task<IEnumerable<Reserva>> ListarVigentesEntre(DateTime primeraNoche, DateTime ultimaNoche);
	    Task<IEnumerable<Reserva>> Listar(ReservaEstadoEnum? estado, DateTime? checkInDesde, DateTime? checkInHasta, DateTime? checkOutDesde, DateTime? checkOutHasta);
        Task<int> Crear(Reserva reserva);
        Task<Reserva> ObtenerPorId(int id);
        Task<int> HacerCheckIn(Reserva reservaModificada);
        Task<int> HacerCheckOut(Reserva reservaModificada);
        Task<int> Cancelar(Reserva reserva);
		Task<int> ObtenerCantidadDeCheckInsDeHoy();
		Task<int> ObtenerCantidadDeCheckOutsDeHoy();
    }
}
