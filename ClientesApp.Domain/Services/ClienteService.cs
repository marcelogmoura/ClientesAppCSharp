using ClientesApp.Domain.Dto;
using ClientesApp.Domain.Entities;
using ClientesApp.Domain.Interfaces.Repositories;
using ClientesApp.Domain.Interfaces.Services;
using ClientesApp.Domain.Validations;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesApp.Domain.Services
{
    public class ClienteService : IClienteService
    {

        private readonly IClienteRepository _clienteRepository;
        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public ClienteResponseDto Alterar(Guid id, ClienteRequestDto dto)
        {
            var cliente = _clienteRepository.GetById(id);

            if (cliente == null)
                throw new ApplicationException("id nao encontrado");

            cliente.Nome = dto.Nome;
            cliente.Email = dto.Email;
            cliente.Cpf = dto.Cpf;
            cliente.DataUltimaAlteracao = DateTime.Now;

            var clienteValidator = new ClienteValidator();
            var result = clienteValidator.Validate(cliente);

            if (!result.IsValid)
                throw new ValidationException(result.Errors);

            if (_clienteRepository.VerifyCpf(dto.Cpf, cliente.Id))
                throw new ApplicationException("esse cpf ja esta cadastrado");

            _clienteRepository.Update(cliente);

            return new ClienteResponseDto
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Email = cliente.Email,
                Cpf = cliente.Cpf,
                DataInclusao = cliente.DataInclusao,
                DataUltimaAlteracao = cliente.DataUltimaAlteracao

            };
        }

        

        public List<ClienteResponseDto> Consultar()
        {
            var response = new List<ClienteResponseDto>();

            var clientes = _clienteRepository.GetAll();

            foreach (var item in clientes)
            {
                response.Add(new ClienteResponseDto
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Email = item.Email,
                    Cpf = item.Cpf,
                    DataInclusao = item.DataInclusao,
                    DataUltimaAlteracao = item.DataUltimaAlteracao
                });
            }

            return response;
        }


        public ClienteResponseDto Excluir(Guid id)
        {
            var cliente = _clienteRepository.GetById(id);
            if (cliente == null)
                throw new ApplicationException("id nao encontrado");

            cliente.Ativo = false;
            cliente.DataUltimaAlteracao = DateTime.Now;

            _clienteRepository.Update(cliente);

            return new ClienteResponseDto
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Email = cliente.Email,
                Cpf = cliente.Cpf,
                DataInclusao = cliente.DataInclusao,
                DataUltimaAlteracao = cliente.DataUltimaAlteracao
            };
        }


        public ClienteResponseDto Incluir(ClienteRequestDto dto)
        {
            var cliente = new Cliente
            {
                Id = Guid.NewGuid(),
                Nome = dto.Nome,
                Cpf = dto.Cpf,
                Email = dto.Email,
                DataInclusao = DateTime.Now,
                DataUltimaAlteracao = DateTime.Now,
                Ativo = true
            };

            var clienteValidator = new ClienteValidator();
            var result = clienteValidator.Validate(cliente);

            #region impedir email e cpf  duplicado

            if (_clienteRepository.VerifyEmail(dto.Email , cliente.Id))
                throw new ApplicationException("esse email ja esta cadastrado");

            if (_clienteRepository.VerifyCpf(dto.Cpf , cliente.Id))
                throw new ApplicationException("cpf ja cadastrado");

            #endregion

            if (!result.IsValid)
                throw new ValidationException(result.Errors);

            if (_clienteRepository.VerifyEmail(dto.Email, cliente.Id))
                throw new ApplicationException("email em uso");

            if (_clienteRepository.VerifyCpf(dto.Cpf, cliente.Id))
                throw new ApplicationException("cpf em uso");

            _clienteRepository.Add(cliente);

            return new ClienteResponseDto
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Cpf = cliente.Cpf,
                Email = cliente.Email,
                DataInclusao = cliente.DataInclusao,
                DataUltimaAlteracao = cliente.DataUltimaAlteracao,

            };
        }

        public ClienteResponseDto ObterPorId(Guid id)
        {
            var cliente = _clienteRepository.GetById(id);

            if (cliente == null)
                throw new ApplicationException("id nao encontrado");

            return new ClienteResponseDto
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Email = cliente.Email,
                Cpf = cliente.Cpf,
                DataInclusao = cliente.DataInclusao,
                DataUltimaAlteracao = cliente.DataUltimaAlteracao
            };
        }
    }
}
