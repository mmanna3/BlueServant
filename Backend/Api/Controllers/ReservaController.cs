﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Controllers.DTOs;
using Api.Core.Models;
using AutoMapper;
using Api.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class ReservasController : ApiAutenticadoController
    {
        private readonly IReservaService _service;
        private readonly IMapper _mapper;

        public ReservasController(IMapper mapper, IReservaService service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<ReservaDTO>> Listar()
        {
            var reservas = await _service.Listar();
            var reservaDTOs = _mapper.Map<IEnumerable<ReservaDTO>>(reservas);

            return reservaDTOs;
        }

        [HttpPost]
        public async Task<int> Crear([FromBody] ReservaDTO dto)
        {
            var reserva = _mapper.Map<Reserva>(dto);
            var id = await _service.Crear(reserva);

            return id;
        }
    }
}
