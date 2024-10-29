using ClientesApp.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesApp.Domain.Validations
{
    public class ClienteValidator : AbstractValidator<Cliente>
    {
        public ClienteValidator() 
        {
            RuleFor(c => c.Id)
                .NotEmpty()
                .WithMessage("O id é obrigatório.");

            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O nome do cliente é obrigatório")
                .Length(2, 20).WithMessage("de 2 a 30 caracteres.");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("O email é obrigatório.")
                .EmailAddress().WithMessage("O email deve ser válido.");

            RuleFor(c => c.Cpf)
                .NotEmpty().WithMessage("O cpf é obrigatório.")
                .Matches(@"^\d{11}$").WithMessage("O cpf deve ter exatamente 11 dígitos.");

            RuleFor(c => c.DataInclusao)
                .NotEmpty().WithMessage("A data de inclusão é obrigatória.");

            RuleFor(c => c.DataUltimaAlteracao)
                .NotEmpty().WithMessage("A data da última alteração é obrigatória.");

            RuleFor(c => c.Ativo)
                .NotEmpty().WithMessage("Ativo/Inativo é obrigatório.");
}
    }
    }


