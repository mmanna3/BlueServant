﻿using System.ComponentModel;

namespace Api.Core.Enums
{
	public enum ReservaEstadoEnum
	{
		[Description("Cancelada")]
		Cancelada = 0,

		[Description("Check-in pendiente")]
		CheckinPendiente = 1,

		[Description("In-House")]
		InHouse = 2,

		[Description("Hizo Check-out")]
		HizoCheckout = 3,
	}
}
