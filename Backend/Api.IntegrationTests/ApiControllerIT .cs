using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Api.Controllers.DTOs;
using Api.Controllers.DTOs.Habitacion;
using FluentAssertions;
using NUnit.Framework;

namespace Api.IntegrationTests
{
    public class ApiControllerIT : BaseAutenticadoIT
    {
        private const string ENDPOINT = "/api/habitaciones";
        private const byte CAMAS_MATRIMONIALES = 2;

        [Test]
        public async Task Error400_PorBodyIncorrectoEnPost()
        {
            var bodySinUnCampoRequerido = new HabitacionDTO
            {
                CamasIndividuales = new List<CamaIndividualDTO>(),
                CamasMatrimoniales = CAMAS_MATRIMONIALES,
                CamasMarineras = 3,
            };

            var response = await _httpClient.PostAsJsonAsync(ENDPOINT, bodySinUnCampoRequerido);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var responseContent = await response.Content.ReadAsStringAsync();

            responseContent.Should().Contain("Nombre");
        }
    }
}