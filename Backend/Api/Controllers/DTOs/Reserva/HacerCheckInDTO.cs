﻿using System.Collections.Generic;
using Api.Controllers.DTOs.Pasajero;

namespace Api.Controllers.DTOs.Reserva
{
    public class HacerCheckInDTO
    {
	    public HacerCheckInDTO()
	    {
		    PasajerosAnexos = new List<PasajeroDTO>();
	    }

        public int ReservaId { get; set; }

        public PasajeroDTO PasajeroTitular { get; set; }

        public List<PasajeroDTO> PasajerosAnexos { get; set; }
    }
}
