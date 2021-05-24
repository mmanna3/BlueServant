using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Api.Controllers.DTOs;
using Api.Controllers.DTOs.Habitacion;
using Api.Controllers.DTOs.Pasajero;
using Api.Controllers.DTOs.Reserva;
using Api.Core;
using Api.Core.Enums;
using FluentAssertions;
using NUnit.Framework;

namespace Api.IntegrationTests
{
	public class ReservasIT : BaseAutenticadoIT
    {
	    private const string ENDPOINT_HABITACIONES = "/api/habitaciones";

	    private const CamaTipoEnum CAMA_TIPO = CamaTipoEnum.Individual;
        private readonly DateTime _desde = new DateTime(2020, 09, 17);
        private readonly DateTime _hasta = new DateTime(2020, 09, 18);
        private readonly PasajeroDTO _pasajero = new PasajeroDTO
        {
	        NombreCompleto = "Elliot Alderson",
	        DniOPasaporte = "123456789",
	        Email = "mrrobot@fsociety.ong",
	        Telefono = "5556453",
            Pais = "AR",
        };

        private ReservasHttpClient _reservasHttpClient;

        protected override void EjecutarUnaSolaVez()
        {
	        _reservasHttpClient = new ReservasHttpClient(_httpClient);
        }

        [Test]
        public async Task Crea_UnaReserva_Y_ApareceEnListado()
        {

            var camaId = await CrearHabitacionConUnaCama();

            var response = await _reservasHttpClient.CrearReserva(camaId, null, _pasajero, _desde, _hasta);
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var consultaResponse = await _reservasHttpClient.ListarEntre(Utilidades.ConvertirFecha(_desde), 1);
            consultaResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var reservasDelMes = await consultaResponse.Content.ReadAsAsync<ReservasDelPeriodoDTO>();

            reservasDelMes.Reservas.Count().Should().Be(1);
            var reserva = reservasDelMes.Reservas.ToList().First();

            reserva.DiaDeCheckin.Should().Be("2020-09-17");
            reserva.DiaDeCheckout.Should().Be("2020-09-17");
            reserva.CamasIds.Should().HaveCount(1);
            reserva.CamasIds.First().Should().Be(camaId);
            reserva.NombreAbreviadoDelHuesped.Should().Be("Elliot");
            reserva.Estado.Should().Be(ReservaEstadoEnum.CheckinPendiente);
        }

        [Test]
        public async Task Crea_UnaReserva_Y_LaObtienePorId()
        {
	        var camaId = await CrearHabitacionConUnaCama();

	        var response = await _reservasHttpClient.CrearReserva(camaId, null, _pasajero, _desde, _hasta);
	        response.StatusCode.Should().Be(HttpStatusCode.OK);
	        var reservaId = await response.Content.ReadAsAsync<int>();
            
	        var consultaResponse = await _reservasHttpClient.ObtenerPorId(reservaId);
	        consultaResponse.StatusCode.Should().Be(HttpStatusCode.OK);
	        var reserva = await consultaResponse.Content.ReadAsAsync<ReservaDetalleDTO>();

	        reserva.DiaDeCheckin.Should().Be("2020-09-17");
	        reserva.DiaDeCheckout.Should().Be("2020-09-18");
	        reserva.HoraEstimadaDeLlegada.Should().Be("11:30");
	        reserva.CantidadDePasajeros.Should().Be(2);
	        reserva.Canal.Should().Be("Booking");
            reserva.Camas.Should().HaveCount(1);
            reserva.Camas.First().Id.Should().Be(camaId);
	        reserva.Estado.Should().Be(ReservaEstadoEnum.CheckinPendiente);

	        reserva.PasajeroTitular.NombreCompleto = _pasajero.NombreCompleto;
	        reserva.PasajeroTitular.Telefono = _pasajero.Telefono;
            reserva.PasajeroTitular.Email = _pasajero.Email;
            reserva.PasajeroTitular.DniOPasaporte = _pasajero.DniOPasaporte;
            reserva.PasajeroTitular.Pais = _pasajero.Pais;

        }

        [Test]
        public async Task DadoQueElHuespedNoexistia_CreaUnaReserva_Y_SeCreaElHuesped()
        {
	        var camaId = await CrearHabitacionConUnaCama();

	        var response = await _reservasHttpClient.CrearReserva(camaId, null, _pasajero, DateTime.Today.AddDays(-1), DateTime.Today);
	        response.StatusCode.Should().Be(HttpStatusCode.OK);

	        var pasajerosResponse = await _reservasHttpClient.ListarHuespedes();
	        pasajerosResponse.StatusCode.Should().Be(HttpStatusCode.OK);
	        var pasajeros = await pasajerosResponse.Content.ReadAsAsync<IEnumerable<PasajeroDTO>>();
	        var pasajero = pasajeros.First();

	        pasajero.DniOPasaporte.Should().Be(_pasajero.DniOPasaporte);
	        pasajero.Email.Should().Be(_pasajero.Email);
	        pasajero.NombreCompleto.Should().Be(_pasajero.NombreCompleto);
	        pasajero.Telefono.Should().Be(_pasajero.Telefono);
        }

        [Test]
        public async Task DadoQueElHuespedYaExistia_CreaUnaReserva_Y_SeModificaElHuesped()
        {
	        var camaId = await CrearHabitacionConUnaCama();
	        var pasajeroId = await _reservasHttpClient.CrearPasajero(_pasajero);

	        var response = await _reservasHttpClient.CrearReserva(camaId, null, _pasajero, DateTime.Today.AddDays(-1), DateTime.Today);
	        response.StatusCode.Should().Be(HttpStatusCode.OK);

	        var huespedesResponse = await _reservasHttpClient.ListarHuespedes();
	        huespedesResponse.StatusCode.Should().Be(HttpStatusCode.OK);
	        var pasajeros = await huespedesResponse.Content.ReadAsAsync<IEnumerable<PasajeroDTO>>();
	        var pasajero = pasajeros.Single(x => x.Id == pasajeroId);

	        pasajero.DniOPasaporte.Should().Be(_pasajero.DniOPasaporte);
	        pasajero.Email.Should().Be(_pasajero.Email);
	        pasajero.NombreCompleto.Should().Be(_pasajero.NombreCompleto);
	        pasajero.Telefono.Should().Be(_pasajero.Telefono);
        }

        [Test]
        public async Task Lista_Correctamente_CheckoutsDeHoy()
        {
            var camaId = await CrearHabitacionConUnaCama();

            await _reservasHttpClient.CrearReserva(camaId, null, _pasajero, DateTime.Today.AddDays(-3), DateTime.Today);

            var consultaResponse = await _reservasHttpClient.ListarCheckoutsDeHoy();
            consultaResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var reservasConCheckoutHoy = await consultaResponse.Content.ReadAsAsync<List<CheckoutsDeHoyDTO>>();

            reservasConCheckoutHoy.Count().Should().Be(1);
            var reserva = reservasConCheckoutHoy.ToList().First();

            //reserva.ANombreDe.Should().Be(A_NOMBRE_DE);
        }

        private async Task<int> CrearHabitacionConUnaCama()
        {
            var body = new HabitacionDTO
            {
                Nombre = "Roja",
                CamasIndividuales = new List<CamaDTO>
                {
                    new CamaDTO
                    {
                        Nombre = "Indios",
                        Tipo = CAMA_TIPO
                    }
                }
            };

            await _httpClient.PostAsJsonAsync(ENDPOINT_HABITACIONES, body);
            var habitacionesDTO = await (await _httpClient.GetAsync(ENDPOINT_HABITACIONES)).Content
                .ReadAsAsync<IEnumerable<HabitacionDTO>>();

            return habitacionesDTO.First().CamasIndividuales.First().Id;
        }
    }
}