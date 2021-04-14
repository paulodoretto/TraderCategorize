using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TradeCategories.Domain.Validators
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(x => x)
                .NotEmpty()
                .WithMessage("A entidade não por ser vazia.")

                .NotNull()
                .WithMessage("A entidade não por ser nula.");

            RuleFor(x => x.Name)
               .NotNull()
               .WithMessage("O nome não pode ser nulo.")

               .NotEmpty()
               .WithMessage("O nome não pode ser vazio.")

               .MinimumLength(3)
               .WithMessage("O nome deve ter no mínimo 3 caracteres.")

               .MaximumLength(80)
               .WithMessage("O nome deve ter no máximo 80 caracteres.");

            RuleFor(x => x.ValueInitial)
                .NotNull()
                .WithMessage("O Valor Inicial não pode ser nulo.")

                .NotEmpty()
                .WithMessage("O Valor Inicial não pode ser vazio.")

                .GreaterThan(0)
                .WithMessage("O Valor Inicial deve er maior que zero")

                .LessThan(x => x.ValueFinal)
                .WithMessage("O Valor Inicial deve ser maior que o Valor Final");

            RuleFor(x => x.ValueFinal)
               .NotNull()
               .WithMessage("O Valor Final não pode ser nulo.")

               .NotEmpty()
               .WithMessage("O Valor Final não pode ser vazio.")

               .GreaterThan(0)
               .WithMessage("O Valor Final deve er maior que zero")

               .GreaterThan(x => x.ValueInitial)
               .WithMessage("O Valor Final deve ser maior que o Valor Inicial");

            RuleFor(x => x.SectorClient).IsInEnum();
        }
    }
}
