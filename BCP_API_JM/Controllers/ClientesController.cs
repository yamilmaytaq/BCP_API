using AutoMapper;
using BCP_API_JM.Data;
using BCP_API_JM.Models;
using BCP_API_JM.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;
using System.Net;
using BCP_API_JM.Models.DTO;

namespace BCP_API_JM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ILogger<ClientesController> _logger;
        private readonly IClientesRepository _clientesRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public ClientesController(ILogger<ClientesController> logger, ApplicationDbContext context, IMapper mapper, IClientesRepository clientesRepo)
        {
            _logger = logger;
            _clientesRepo = clientesRepo;
            _mapper = mapper;
            _response = new();
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetClients()
        {
            try
            {
                _logger.LogInformation("Obtener todos los clientes");

                IEnumerable<BD_CLIENTES> TestList = await _clientesRepo.GetAll();
                _response.Result = _mapper.Map<IEnumerable<BD_CLIENTES_DTO>>(TestList);
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

        [HttpGet("id:int", Name = "GetClient")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BD_CLIENTES_DTO>> GetClient(int id)
        {
            try
            {
                var client = await _clientesRepo.Get(t => t.Id == id);
                if (id == 0)
                {
                    _logger.LogError("Error al obtener el cliente con el ID: " + id);
                    return BadRequest();
                }

                if (client == null)
                {
                    _logger.LogError("No se encontro ningun cliente con el ID: " + id);
                    return NotFound();
                }

                _logger.LogError("Se obtuvo satisfactoriamente el cliente con el ID: " + id);
                _response.Result = _mapper.Map<BD_CLIENTES_DTO>(client);
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

        public async Task<ActionResult<BD_CLIENTES_DTO>> CreateClient([FromBody] BD_CLIENTES_CREATE_DTO clientCreateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _clientesRepo.Get(t => t.CI.ToString() == clientCreateDto.CI.ToString()) != null)
                {
                    _logger.LogError("Error al crear CI existente.");
                    ModelState.AddModelError("Exist", "El cliente con ese carnet de identidad ya existe, favor ingresar otro dato.");
                    return BadRequest(ModelState);
                }
                if (await _clientesRepo.Get(t => t.Cuenta == clientCreateDto.Cuenta) != null)
                {
                    _logger.LogError("Error al crear Nro Cuenta existente.");
                    ModelState.AddModelError("Exist", "El cliente con ese Nro de cuenta ya existe, favor ingresar otro dato.");
                    return BadRequest(ModelState);
                }

                if (clientCreateDto == null)
                {
                    return BadRequest(clientCreateDto);
                }

                BD_CLIENTES model = _mapper.Map<BD_CLIENTES>(clientCreateDto);

                await _clientesRepo.Create(model);

                return CreatedAtRoute("GetClient", new { id = model.Id }, model);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<String>() { ex.ToString() };
            }
            return Ok(_response);
            
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTest(int id)
        {
            try
            {
                var client = await _clientesRepo.Get(t => t.Id == id);
                if (id == 0)
                {
                    _logger.LogError("Error al ingresar ID invalido.");
                    ModelState.AddModelError("Error", "El cliente con ese ID no es valido, favor ingresar otro dato.");
                    return BadRequest();
                }
                if (client == null)
                {
                    _logger.LogError("Contenido Nulo.");
                    ModelState.AddModelError("Error", "El cliente no existe, favor ingresar otro dato.");
                    return NotFound();
                }
                await _clientesRepo.Remove(client);

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
        public async Task<IActionResult> UpdateTest(int id, [FromBody] BD_CLIENTES_UPDATE_DTO clientUpdateDto)
        {
            try
            {
                if (clientUpdateDto == null || id != clientUpdateDto.Id)
                {
                    return BadRequest();
                }
                BD_CLIENTES modelTest = _mapper.Map<BD_CLIENTES>(clientUpdateDto);


                await _clientesRepo.Update(modelTest);

                return NoContent();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<String>() { ex.ToString() };
            }
            return Ok(_response);

        }
    }
}

