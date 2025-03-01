using System;
using FluentValidation;
using BillingMVC.Core.Entities;

namespace BillingMVC.Core.Validators
{
    public class BillFilterValidator : AbstractValidator<BillFilter>
    {
        public BillFilterValidator()
        {
            this.RuleFor(f => f.DateRangeStart)
                .GreaterThan(DateTime.Now.AddYears(-1))
                .WithMessage("Não é possível listar " +
                "despesas de mais de 1 ano atrás.");

            this.RuleFor(f => f.DateRangeStart)
                .LessThan(f => f.DateRangeEnd)
                .WithMessage("O intervalo inicial de " +
                "data da despesa não pode ser maior " +
                "que o intervalo final.");

            this.RuleFor(f => f.ValueRangeStart)
                .LessThan(f => f.ValueRangeEnd)
                .WithMessage("O intervalo inicial de " +
                "valor da despesa não pode ser maior " +
                "que o intervalo final.");

            this.RuleFor(f => f.ValueRangeStart)
                .GreaterThan(0)
                .WithMessage("O intervalo inicial de " +
                "valor da despesa não pode ser menor " +
                "que zero.");

            this.RuleFor(f => f.ValueRangeEnd)
                .LessThan(1000000)
                .WithMessage("O intervalo final de " +
                "valor da despesa não pode ser maior " +
                "que 1 milhão.");
        }
    }
}