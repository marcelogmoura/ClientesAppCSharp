using ClientesApp.Domain.Dto;
using ClientesApp.Domain.Interfaces.Services;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClientesApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ClienteResponseDto), 201)]
        public IActionResult Post([FromBody] ClienteRequestDto dto)
        {
            try
            {
                var response = _clienteService.Incluir(dto);
                return StatusCode(201, response);
            }
            catch (ValidationException e)
            {
                var errors = e.Errors.Select(e => new
                {
                    Name = e.PropertyName,
                    Error = e.ErrorMessage
                });

                return StatusCode(400, errors);
            }
            catch (ApplicationException e)
            {
                return StatusCode(422, new { e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { e.Message });
            }
        }


        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ClienteResponseDto), 200)]
        public IActionResult Put(Guid id, [FromBody]  ClienteRequestDto dto)
        {
            try
            {
                var response = _clienteService.Alterar(id, dto);
                return StatusCode(200, response);
            }
            catch (ValidationException e)
            {
                var errors = e.Errors.Select
                    (e => new
                    {
                        Name = e.PropertyName,
                        Error = e.ErrorMessage
                    });

                return StatusCode(400, errors);
            }
            catch (ApplicationException e)
            {
                return StatusCode(422, new { e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { e.Message });
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ClienteResponseDto), 200)]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var response = _clienteService.Excluir(id);
                return StatusCode(200, response);
            }
            catch (ApplicationException e)
            {
                return StatusCode(422, new { e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { e.Message });
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ClienteResponseDto>), 200)]
        public IActionResult GetAll()
        {
            try
            {
                var response = _clienteService.Consultar();
                return StatusCode(200, response);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { e.Message });
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ClienteResponseDto), 200)]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var response = _clienteService.ObterPorId(id);
                return StatusCode(200, response);
            }
            catch (ApplicationException e)
            {
                return StatusCode(422, new { e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { e.Message });
            }
        }
    }
}