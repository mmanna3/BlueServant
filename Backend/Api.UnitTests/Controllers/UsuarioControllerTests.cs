using System.Threading.Tasks;
using Api.Controllers;
using Api.Controllers.DTOs.Usuario;
using Api.Core.Entidades;
using Api.Core.Services.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Api.UnitTests.Controllers
{
    public class UsuarioControllerTests
    {
        private const string USERNAME = "Elliot";
        private const string PASSWORD = "Alderson";

        private UsuariosController _controller;
        private Mock<IUsuarioService> _mockService;

        private Usuario _unUsuario;
        private RegistrarDTO _unRegistrarDto;

        [SetUp]
        public void Inicializar()
        {
	        _mockService = new Mock<IUsuarioService>();
            _controller = new UsuariosController(_mockService.Object);
        }

        [Test]
        public async Task Registra_Ok()
        {
            DadoUnRegistroResource();
            DadoUnUsuario();
            DadoQueElServicioDevuelveUnUsuario();

            var resultado = await _controller.Registrar(_unRegistrarDto);

            var okObjectResult = resultado.Should().BeOfType<OkObjectResult>().Subject;
            okObjectResult.Value.Should().BeAssignableTo<RegistrarDTO>();
        }

        private void DadoQueElServicioDevuelveUnUsuario()
        {
            _mockService.Setup(x => x.Crear(It.IsAny<Usuario>(), It.IsAny<string>())).ReturnsAsync(_unUsuario);
        }

        private void DadoUnRegistroResource()
        {
            _unRegistrarDto = new RegistrarDTO
            {
                Nombre = "Jackson",
                Apellido = "Watmore",
                Username = USERNAME,
                Password = PASSWORD
            };
        }

        private void DadoUnUsuario()
        {
            _unUsuario = new Usuario
            {
                Username = USERNAME
            };
        }
    }
}