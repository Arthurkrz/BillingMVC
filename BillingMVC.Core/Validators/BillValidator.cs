using BillingMVC.Core.Entities;
using BillingMVC.Core.Enum;
using FluentValidation;
using System;

namespace BillingMVC.Core.Validators
{
    public class BillValidator : AbstractValidator<Bill>
    {
        public BillValidator()
        {
            this.RuleFor(b => b.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Insira um nome.");

            this.RuleFor(b => b.ExpirationDate)
                .Must(b => b != default)
                .WithMessage("Insira uma data de " +
                             "vencimento da conta.");

            this.RuleFor(b => b.ExpirationDate)
                .GreaterThan(DateTime.Now.AddYears(-1))
                .WithMessage("Contas vencidas a mais " +
                             "de 1 ano não podem " +
                             "ser adicionadas.");

            this.RuleFor(b => b.ExpirationDate)
                .Must((request, date) => request.IsPaid != 
                CustomBoolean.No && date < DateTime.Now.AddMonths(-6))
                .WithMessage("Contas não pagas vencidas a " +
                             "mais de 6 meses não podem " +
                             "ser adicionadas.");

            this.RuleFor(b => b.Source)
                .NotEmpty()
                .NotNull()
                .WithMessage("Insira a origem da conta.");

            this.RuleFor(b => b.IsPaid)
                .NotEqual(CustomBoolean.NA)
                .WithMessage("Especifique se a conta " +
                             "foi paga ou não.");

            this.RuleFor(b => b.Value)
                .NotEqual(0)
                .WithMessage("Especifique o valor da conta.");

            this.RuleFor(b => b.Value)
                .LessThan(1000000)
                .WithMessage("O valor da conta não pode " +
                             "ser maior que R$ 1 milhão.");

            this.RuleFor(b => b.Currency)
                .NotNull()
                .WithMessage("Especifique a moeda.");

            this.RuleFor(b => b.Type)
                .NotNull()
                .WithMessage("Especifique a categoria da conta.");
        }
    }
}
