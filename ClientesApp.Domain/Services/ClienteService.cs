using ClientesApp.Domain.Dto;
using ClientesApp.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesApp.Domain.Services
{
    public class ClienteService : IClienteService
    {
        ClienteResponseDto IClienteService.Alterar(Guid id, ClienteRequestDto dto)
        {
            throw new NotImplementedException();
        }

        List<ClienteResponseDto> IClienteService.Consultar()
        {
            throw new NotImplementedException();
        }

        ClienteResponseDto IClienteService.Excluir(Guid id)
        {
            throw new NotImplementedException();
        }

        ClienteResponseDto IClienteService.Incluir(ClienteRequestDto dto)
        {
            throw new NotImplementedException();
        }

        ClienteResponseDto IClienteService.ObterPorId(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
