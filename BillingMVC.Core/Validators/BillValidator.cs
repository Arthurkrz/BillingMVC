using BillingMVC.Core.Entities;
using FluentValidation;
using System;

namespace BillingMVC.Core.Validators
{
    public class BillValidator : AbstractValidator<Bill>
    {
        public BillValidator()
        {
            this.RuleFor(b => b.Name).Must(n => 
                       !string.IsNullOrEmpty(n))
               .WithMessage("Insira um nome.");

            this.RuleFor(b => b.PurchaseDate)
                .NotNull().NotEqual(default(DateTime))
                .WithMessage("Insira a data da " +
                             "despesa.");

            this.RuleFor(b => b.PurchaseDate)
                .GreaterThan(DateTime.Now.AddYears(-1))
                .WithMessage("Despesas de mais de 1 ano atrás não podem ser adicionadas.");

            this.RuleFor(b => b.Source).Must(n => 
                          !string.IsNullOrEmpty(n))
                .WithMessage("Insira a origem da despesa.");

            this.RuleFor(b => b.Value)
                .NotEqual(0)
                .WithMessage("Especifique o valor da despesa.");

            this.RuleFor(b => b.Value)
                .LessThan(1000000)
                .WithMessage("O valor da despesa não pode " +
                             "ser maior que R$ 1 milhão.");

            this.RuleFor(b => b.Currency)
                .NotNull()
                .WithMessage("Especifique a moeda.");

            this.RuleFor(b => b.Type)
                .NotNull()
                .WithMessage("Especifique a categoria da despesa.");
        }
    }
}
