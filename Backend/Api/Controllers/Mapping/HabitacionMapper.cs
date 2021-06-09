﻿using Api.Core.Entidades;
using System.Collections.Generic;
using System.Linq;
using Api.Controllers.DTOs.Habitacion;
using Api.Core.Enums;

namespace Api.Controllers.Mapping
{
	public static class HabitacionMapper
	{
		public static HabitacionDetalleDTO MapDetalle(Habitacion habitacion)
		{
			return new HabitacionDetalleDTO
			{
				Id = habitacion.Id,
				Nombre = habitacion.Nombre,
				TieneBanio = habitacion.TieneBanio,
				EsPrivada = habitacion.Tipo().Equals(HabitacionTipoEnum.Privada),
				InformacionAdicional = habitacion.InformacionAdicional,
				Camas = habitacion.ObtenerTodasLasCamas().Select(x => new CamaDTO { Id = x.Id, Nombre = x.Nombre, Tipo = x.Tipo() }).ToList()
			};
		}

		public static HabitacionDTO Map(Habitacion entidad)
		{
			return new HabitacionDTO
			{
				Id = entidad.Id,
				Nombre = entidad.Nombre,
				TieneBanio = entidad.TieneBanio,
				EsPrivada = entidad.Tipo().Equals(HabitacionTipoEnum.Privada),
				InformacionAdicional = entidad.InformacionAdicional,
				CamasIndividuales = entidad.CamasIndividuales.Select(entidadCamasIndividuale => new CamaDTO
				{
					Id = entidadCamasIndividuale.Id,
					Nombre = entidadCamasIndividuale.Nombre,
					Tipo = entidadCamasIndividuale.Tipo()
				}).ToList(),
				CamasCuchetas = entidad.CamasCuchetas.Select(entidadCamasCucheta => new CamaCuchetaDTO
				{
					Id = entidadCamasCucheta.Id,
					Abajo = new CamaDTO
					{
						Id = entidadCamasCucheta.Abajo.Id,
						Nombre = entidadCamasCucheta.Abajo.Nombre,
						Tipo = entidadCamasCucheta.Abajo.Tipo()
					},
					Arriba = new CamaDTO
					{
						Id = entidadCamasCucheta.Arriba.Id,
						Nombre = entidadCamasCucheta.Arriba.Nombre,
						Tipo = entidadCamasCucheta.Arriba.Tipo()
					}
				}).ToList(),
				CamasMatrimoniales = entidad.CamasMatrimoniales.Select(entidadCamasMatrimoniale => new CamaDTO
				{
					Id = entidadCamasMatrimoniale.Id,
					Nombre = entidadCamasMatrimoniale.Nombre,
					Tipo = entidadCamasMatrimoniale.Tipo()
				}).ToList()
			};
		}

		public static CamaDTO MapCama(Cama cama)
		{
			return new CamaDTO
			{
				Id = cama.Id,
				Tipo = cama.Tipo(),
				Nombre = cama.Nombre,
				NombreHabitacion = cama.ObtenerHabitacion().Nombre,
		};
		}

		public static Habitacion Map(HabitacionDTO dto)
		{
			if (dto.EsPrivada)
				return new HabitacionPrivada
				{
					Nombre = dto.Nombre,
					TieneBanio = dto.TieneBanio,
					InformacionAdicional = dto.InformacionAdicional,
					CamasIndividuales = dto.CamasIndividuales?.ConvertAll(camaIndividual => new CamaIndividual
					{
						Nombre = camaIndividual.Nombre
					}),
					CamasMatrimoniales = dto.CamasMatrimoniales?.ConvertAll(camaMatrimonial => new CamaMatrimonial
					{
						Nombre = camaMatrimonial.Nombre
					}),
					CamasCuchetas = dto.CamasCuchetas?.ConvertAll(dtoCamasCucheta => new CamaCucheta
					{
						Abajo = new CamaCuchetaDeAbajo
						{
							Nombre = dtoCamasCucheta.Nombre
						},
						Arriba = new CamaCuchetaDeArriba
						{
							Nombre = dtoCamasCucheta.Nombre
						}
					}),
				};
			else 
				return new HabitacionCompartida
				{
					Nombre = dto.Nombre,
					TieneBanio = dto.TieneBanio,
					InformacionAdicional = dto.InformacionAdicional,
					CamasIndividuales = dto.CamasIndividuales?.ConvertAll(camaIndividual => new CamaIndividual
					{
						Nombre = camaIndividual.Nombre
					}),
					CamasMatrimoniales = dto.CamasMatrimoniales?.ConvertAll(camaMatrimonial => new CamaMatrimonial
					{
						Nombre = camaMatrimonial.Nombre
					}),
					CamasCuchetas = dto.CamasCuchetas?.ConvertAll(dtoCamasCucheta => new CamaCucheta
					{
						Abajo = new CamaCuchetaDeAbajo
						{
							Nombre = dtoCamasCucheta.Nombre
						},
						Arriba = new CamaCuchetaDeArriba
						{
							Nombre = dtoCamasCucheta.Nombre
						}
					}),
				};
		}

		public static IEnumerable<CamaDTO> MapCamas(IEnumerable<Cama> camas)
		{
			return camas.Select(MapCama);
		}

		public static IEnumerable<HabitacionDTO> Map(IEnumerable<Habitacion> habitaciones)
		{
			return habitaciones.Select(Map);
		}

		public static HabitacionConLugaresLibresDTO MapHabitacionesConLugaresLugares(Habitacion habitacion)
		{
			return new HabitacionConLugaresLibresDTO
			{
				Id = habitacion.Id,
				Nombre = habitacion.Nombre,
				CantidadDeLugaresLibres = habitacion.CantidadTotalDeLugaresDisponibles(),
				EsPrivada = habitacion.Tipo().Equals(HabitacionTipoEnum.Privada),
				Camas = habitacion.ObtenerTodasLasCamas().Select(x => new CamaDTO { Id = x.Id, Nombre = x.Nombre, Tipo = x.Tipo() }).ToList()
			};
		}

		public static IEnumerable<HabitacionConLugaresLibresDTO> MapHabitacionesConLugaresLugares(IEnumerable<Habitacion> habitaciones)
		{
			return habitaciones.Select(MapHabitacionesConLugaresLugares);
		}
	}
}
