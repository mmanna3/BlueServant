﻿using System.Threading.Tasks;
using Api.Controllers.DTOs.Usuario;
using Api.Controllers.Mapping;
using Microsoft.AspNetCore.Mvc;
using Api.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;


namespace Api.Controllers
{
	public class UsuariosController : ApiAutenticadoController
    {
        private readonly IUsuarioService _userService;

        public UsuariosController(IUsuarioService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("autenticar")]
        public async Task<IActionResult> Autenticar([FromBody] AutenticarDTO model)
        {
            var usuario = await _userService.Autenticar(model.Username, model.Password);

            var token = _userService.ObtenerToken(usuario.Id);

            return Ok(new
            {
                Id = usuario.Id,
                Username = usuario.Username,
                FirstName = usuario.Nombre,
                LastName = usuario.Apellido,
                Token = token
            });
        }

        [AllowAnonymous]
        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] RegistrarDTO dto)
        {
            var usuario = UsuarioMapper.Map(dto);

            var usuarioCreado = await _userService.Crear(usuario, dto.Password);

            var registrarDTO = UsuarioMapper.Map(usuarioCreado);

            return Ok(registrarDTO);
        }

        [HttpGet("okbro")]
        public IActionResult GetAll()
        {
            return Ok("OK bro");
        }

        //[HttpGet("{id}")]
        //public IActionResult ObtenerPorId(int id)
        //{
        //    var user = _userService.ObtenerPorId(id);
        //    var model = _mapper.Map<UserModel>(user);
        //    return Ok(model);
        //}

        //[HttpPut("{id}")]
        //public IActionResult Update(int id, [FromBody] UpdateModel model)
        //{
        //    // map model to entity and set id
        //    var user = _mapper.Map<User>(model);
        //    user.Id = id;

        //    try
        //    {
        //        // update user 
        //        _userService.Update(user, model.Password);
        //        return Ok();
        //    }
        //    catch (AppException ex)
        //    {
        //        // return error message if there was an exception
        //        return BadRequest(new { message = ex.Message });
        //    }
        //}

        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    _userService.Delete(id);
        //    return Ok();
        //}
    }
}
