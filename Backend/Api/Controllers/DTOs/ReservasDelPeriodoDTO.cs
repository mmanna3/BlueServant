﻿using System.Collections.Generic;

namespace Api.Controllers.DTOs
{
    public class ReservasDelPeriodoDTO
    {
        public List<ReservaResumenDTO> Reservas { get; set; }
        public string Desde { get; set; }
        public string Hasta { get; set; }

        public class ReservaResumenDTO
        {
	        public int Id { get; set; }
            public int DiaInicio { get; set; }
            public int DiaFin { get; set; }
            public string ANombreDe { get; set; }
            public List<int> CamasIds { get; set; }
        }
    }
}