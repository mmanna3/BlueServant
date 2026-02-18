using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Api.Controllers.DTOs.Usuario;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Api.IntegrationTests
{
    public class UsuarioIT : BaseAutenticadoIT
    {
        private const string USERNAME = "MrRobot";
        private const string PASSWORD = "RosaBlanca";

        [SetUp]
        public override async Task Setup()
        {
            await ResetearBaseDeDatosExcluyendoMigraciones();
            await InicializarHttpClientAutenticado();
        }

        protected override async Task InicializarHttpClientAutenticado()
        {
            _httpClient = _server.CreateClient();
            await Task.CompletedTask;
        }

        [Test]
        public async Task Error400_PorBodyIncorrectoEnRegistro()
        {
            var body = new RegistrarDTO
            {
                Nombre = "Elliot",
                Apellido = "Alderson",
                Password = PASSWORD
            };

            var response = await _httpClient.PostAsJsonAsync("/api/usuarios/registrar", body);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var responseContent = await response.Content.ReadAsStringAsync();
            responseContent.Should().Contain("Username");
        }

        [Test]
        public async Task RegistraUnUsuario()
        {
            var response = await RegistrarUnUsuario();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task AutenticaYLuegoAccedeConSuToken_DadoQueEstaRegistrado()
        {
            await DadoQueHayUnUsuarioRegistrado();
            var autenticacionResponse = await DadoQueElUsuarioRegistradoEstaAutenticado();

            dynamic response = await autenticacionResponse.Content.ReadAsAsync<JObject>();
            string token = response.token.ToString();

            await AccederConTokenAUnMetodoAutenticado(token);
        }

        [Test]
        public async Task Error500_AlRegistrarDosVecesAlUsuario()
        {
            await RegistrarUnUsuario();
            
            var response2 = await RegistrarUnUsuario();

            response2.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
            var error = await response2.Content.ReadAsStringAsync();
            error.Should().Contain("Ya existe un usuario");
        }

        [Test]
        public async Task Error401_AlIngresarSinTokenAUnMetodo()
        {
            var response = await AccederSinTokenAUnMetodoAutenticado();

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        private async Task DadoQueHayUnUsuarioRegistrado()
        {
            await RegistrarUnUsuario();
        }

        private async Task<HttpResponseMessage> RegistrarUnUsuario()
        {
            var body = new RegistrarDTO
            {
                Nombre = "Elliot",
                Apellido = "Alderson",
                Username = USERNAME,
                Password = PASSWORD
            };

            return await _httpClient.PostAsJsonAsync("/api/usuarios/registrar", body);
        }

        private async Task<HttpResponseMessage> DadoQueElUsuarioRegistradoEstaAutenticado()
        {
            var response = await AutenticarUnUsuario();

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            return response;
        }

        private async Task<HttpResponseMessage> AutenticarUnUsuario()
        {
            var body = new
            {
                username = USERNAME,
                password = PASSWORD
            };

            return await _httpClient.PostAsJsonAsync("/api/usuarios/autenticar", body);
        }

        private async Task<HttpResponseMessage> AccederConTokenAUnMetodoAutenticado(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync("/api/usuarios/okbro");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            return response;
        }

        private async Task<HttpResponseMessage> AccederSinTokenAUnMetodoAutenticado()
        {
            return await _httpClient.GetAsync("/api/usuarios/okbro");
        }
    }
}