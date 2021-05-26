using System;
using System.Collections.Generic;
using Api.Core.Entidades;
using Api.Core.Enums;
using FluentAssertions;
using NUnit.Framework;

namespace Api.UnitTests.Entidades
{
    public class CamaTests
    {
        private static readonly DateTime INICIO_RESERVA_1 = new DateTime(2020, 09, 03);
        private static readonly DateTime FIN_RESERVA_1 = new DateTime(2020, 09, 07);
        private static readonly DateTime INICIO_RESERVA_2 = new DateTime(2020, 09, 09);
        private static readonly DateTime FIN_RESERVA_2 = new DateTime(2020, 09, 13);

        [Test]
        public void NoEstaLibre_Porque_EseDia_EstaReservado()
        {
            var cama = DadaUnaCamaConVariasReservasVigentes();

            cama.EstaLibreEntre(INICIO_RESERVA_1, INICIO_RESERVA_1).Should().BeFalse();
        }

        [Test]
        public void EstaLibre_Porque_EseDia_EstaReservado_PeroLaReservaEstaCancelada()
        {
	        var cama = DadaUnaCamaConVariasReservasCanceladas();

	        cama.EstaLibreEntre(INICIO_RESERVA_1, INICIO_RESERVA_1).Should().BeTrue();
        }

        [Test]
        public void NoEstaLibre_Porque_TodosEsosDias_EstanReservados()
        {
            var cama = DadaUnaCamaConVariasReservasVigentes();

            cama.EstaLibreEntre(INICIO_RESERVA_1.AddDays(1), INICIO_RESERVA_1.AddDays(3)).Should().BeFalse();
        }

        [Test]
        public void NoEstaLibre_Porque_AlgunosDeTodosEsosDias_EstanReservadosEnUnaReserva()
        {
            var cama = DadaUnaCamaConVariasReservasVigentes();

            cama.EstaLibreEntre(INICIO_RESERVA_1.AddDays(-2), INICIO_RESERVA_1).Should().BeFalse();
        }

        [Test]
        public void NoEstaLibre_Porque_ElRangoIncluyeOtrasReservas()
        {
            var cama = DadaUnaCamaConVariasReservasVigentes();

            cama.EstaLibreEntre(INICIO_RESERVA_1.AddDays(-3), FIN_RESERVA_2.AddDays(3)).Should().BeFalse();
        }

        [Test]
        public void NoEstaLibre_Porque_IncluyeDiasDeDosReservas()
        {
            var cama = DadaUnaCamaConVariasReservasVigentes();

            cama.EstaLibreEntre(INICIO_RESERVA_1.AddDays(1), FIN_RESERVA_2.AddDays(-1)).Should().BeFalse();
        }

        [Test]
        public void NoEstaLibre_Porque_EmpiezaDiaDisponibleYTerminaDiaReservado()
        {
            var cama = DadaUnaCamaConVariasReservasVigentes();

            cama.EstaLibreEntre(INICIO_RESERVA_1.AddDays(-1), INICIO_RESERVA_1).Should().BeFalse();
        }

        [Test]
        public void NoEstaLibre_Porque_EmpiezaDiaReservadoYTerminaDiaDisponible()
        {
            var cama = DadaUnaCamaConVariasReservasVigentes();

            cama.EstaLibreEntre(INICIO_RESERVA_1.AddDays(1), FIN_RESERVA_1.AddDays(1)).Should().BeFalse();
        }

        [Test]
        public void EstaLibre_Porque_EseDia_NoEstaReservado()
        {
            var cama = DadaUnaCamaConVariasReservasVigentes();

            cama.EstaLibreEntre(FIN_RESERVA_1.AddDays(1), FIN_RESERVA_1.AddDays(1)).Should().BeTrue();
        }

        [Test]
        public void EstaLibre_Porque_NingunoDeTodosEsosDias_EstaReservado()
        {
            var cama = DadaUnaCamaConVariasReservasVigentes();

            cama.EstaLibreEntre(FIN_RESERVA_2.AddDays(1), FIN_RESERVA_2.AddDays(6)).Should().BeTrue();
        }

        private static CamaIndividual DadaUnaCamaConVariasReservasVigentes()
        {
            var cama = new CamaIndividual();
            var reserva1 = new Reserva
            {
                PrimeraNoche = INICIO_RESERVA_1,
                UltimaNoche = FIN_RESERVA_1,
                Estado = ReservaEstadoEnum.CheckinPendiente
            };
            var reservaDeCama1 = new ReservaCama
            {
                Reserva = reserva1,
                Cama = cama
            };

            var reserva2 = new Reserva
            {
                PrimeraNoche = INICIO_RESERVA_2,
                UltimaNoche = FIN_RESERVA_2,
                Estado = ReservaEstadoEnum.CheckinPendiente
            };
            var reservaDeCama2 = new ReservaCama
            {
                Reserva = reserva2,
                Cama = cama
            };

            cama.ReservaCamas = new List<ReservaCama> {reservaDeCama1, reservaDeCama2 };

            return cama;
        }

        private static CamaIndividual DadaUnaCamaConVariasReservasCanceladas()
        {
	        var cama = new CamaIndividual();
	        var reserva1 = new Reserva
	        {
		        PrimeraNoche = INICIO_RESERVA_1,
		        UltimaNoche = FIN_RESERVA_1,
                Estado = ReservaEstadoEnum.Cancelada
	        };
	        var reservaDeCama1 = new ReservaCama
	        {
		        Reserva = reserva1,
		        Cama = cama,
	        };

	        var reserva2 = new Reserva
	        {
		        PrimeraNoche = INICIO_RESERVA_2,
		        UltimaNoche = FIN_RESERVA_2,
		        Estado = ReservaEstadoEnum.Cancelada
            };
	        var reservaDeCama2 = new ReservaCama
	        {
		        Reserva = reserva2,
		        Cama = cama
	        };

	        cama.ReservaCamas = new List<ReservaCama> { reservaDeCama1, reservaDeCama2 };

	        return cama;
        }
    }
}