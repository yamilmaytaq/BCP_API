using AutoMapper;
using BCP_API_JM.Data;
using BCP.Shared.Models.DTO;
using BCP.Shared.Models;
using BCP_API_JM.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Cryptography;
using BCP_API_JM.Utils;

namespace BCP_API_JM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ILogger<UsuariosController> _logger;
        private readonly IUsuariosRepository _usuariosRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public UsuariosController(ILogger<UsuariosController> logger, ApplicationDbContext context, IMapper mapper, IUsuariosRepository usuariosRepo)
        {
            _logger = logger;
            _usuariosRepo = usuariosRepo;
            _mapper = mapper;
            _response = new();
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetUsers()
        {
            try
            {
                _logger.LogInformation("Obtener todos los usuarios");

                IEnumerable<BD_USUARIOS> UserList = await _usuariosRepo.GetAll();
                _response.Result = _mapper.Map<IEnumerable<BD_USUARIOS_DTO>>(UserList);
                _response.statusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<String>() { ex.ToString() };
            }
            return _response;

        }

        [HttpGet("id:int", Name = "GetUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BD_USUARIOS_DTO>> GetUser(int id)
        {
            try
            {
                var user = await _usuariosRepo.Get(t => t.Id == id);
                if (id == 0)
                {
                    _logger.LogError("Error al obtener el usuario con el ID: " + id);
                    return BadRequest();
                }

                if (user == null)
                {
                    _logger.LogError("No se encontro ningun usuario con el ID: " + id);
                    return NotFound();
                }

                _logger.LogInformation("Se obtuvo satisfactoriamente el usuario con el ID: " + id);
                _response.Result = _mapper.Map<BD_USUARIOS_DTO>(user);
                _response.statusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<String>() { ex.ToString() };
            }
            return Ok(_response);

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<BD_USUARIOS_DTO>> CreateUser([FromBody] BD_USUARIOS_CREATE_DTO userCreateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _usuariosRepo.Get(t => t.Usuario == userCreateDto.Usuario) != null)
                {
                    _logger.LogError("Error al crear Usuario existente.");
                    ModelState.AddModelError("Exist", "El usuario con el nombre de usuario ya existe, favor ingresar otro dato.");
                    return BadRequest(ModelState);
                }

                var hashedPassword = PasswordUtil.HashPassword(userCreateDto.Contrasenia);

                BD_USUARIOS model = _mapper.Map<BD_USUARIOS>(userCreateDto);
                model.Contrasenia = hashedPassword;

                

                await _usuariosRepo.Create(model);

                return CreatedAtRoute("GetUser", new { id = model.Id }, model);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return Ok(_response);
        }

        


        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var user = await _usuariosRepo.Get(t => t.Id == id);
                if (id == 0)
                {
                    _logger.LogError("Error al ingresar ID invalido.");
                    ModelState.AddModelError("Error", "El usuario con ese ID no es valido, favor ingresar otro dato.");
                    return BadRequest();
                }
                if (user == null)
                {
                    _logger.LogError("Contenido Nulo.");
                    ModelState.AddModelError("Error", "El usuario no existe, favor ingresar otro dato.");
                    return NotFound();
                }
                await _usuariosRepo.Remove(user);

                return NoContent();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<String>() { ex.ToString() };
            }
            return Ok(_response);

        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] BD_USUARIOS_UPDATE_DTO userUpdateDto)
        {
            try
            {
                if (userUpdateDto == null || id != userUpdateDto.Id)
                {
                    return BadRequest();
                }
                BD_USUARIOS model = _mapper.Map<BD_USUARIOS>(userUpdateDto);


                await _usuariosRepo.Update(model);

                return NoContent();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<String>() { ex.ToString() };
            }
            return Ok(_response);

        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<APIResponse>> Login([FromBody] BD_USUARIOS_LOGIN_DTO loginDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var user = await _usuariosRepo.Get(u => u.Usuario == loginDto.Usuario);

                if (user == null || !PasswordUtil.VerifyPassword(loginDto.Contrasenia, user.Contrasenia))
                {
                    return Unauthorized(new APIResponse
                    {
                        IsSuccess = false,
                        statusCode = HttpStatusCode.Unauthorized,
                        ErrorMessages = new List<string> { "Usuario o contraseña incorrectos." }
                    });
                }

                _response.Result = _mapper.Map<BD_USUARIOS_DTO>(user);
                _response.IsSuccess = true;
                _response.statusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }


    }
}
