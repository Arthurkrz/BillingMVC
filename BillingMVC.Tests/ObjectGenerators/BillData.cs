using BillingMVC.Core.Entities;
using BillingMVC.Core.Enum;
using Bogus;
using System.Collections.Generic;
using System;

namespace BillingMVC.Tests.ObjectGenerators
{
    public class BillData
    {
        public static IEnumerable<object[]> GetInvalidBills()
        {
            var faker = new Faker();

            yield return new object[]
            {
                new Bill()
                {
                    Name = null,
                    Currency = faker.PickRandom<Currency>(),
                    Value = faker.Random.Double(1, 1000000),
                    Type = faker.PickRandom<BillType>(),
                    ExpenseDate = faker.Date.Future(1, DateTime.Now),
                    Source = faker.Random.Word()
                },
                "Insira um nome."
            };

            yield return new object[]
            {
                new Bill()
                {
                    Name = faker.Name.FirstName(),
                    Currency = null,
                    Value = faker.Random.Double(1, 1000000),
                    Type = faker.PickRandom<BillType>(),
                    ExpenseDate = faker.Date.Future(1, DateTime.Now),
                    Source = faker.Random.Word()
                },
                "Especifique a moeda."
            };

            yield return new object[]
            {
                new Bill()
                {
                    Name = faker.Name.FirstName(),
                    Currency = faker.PickRandom<Currency>(),
                    Value = 0,
                    Type = faker.PickRandom<BillType>(),
                    ExpenseDate = faker.Date.Future(1, DateTime.Now),
                    Source = faker.Random.Word()
                },
                "Especifique o valor da despesa."
            };

            yield return new object[]
            {
                new Bill()
                {
                    Name = faker.Name.FirstName(),
                    Currency = faker.PickRandom<Currency>(),
                    Value = faker.Random.Double(1, 1000000),
                    Type = null,
                    ExpenseDate = faker.Date.Future(1, DateTime.Now),
                    Source = faker.Random.Word()
                },
                "Especifique a categoria da despesa."
            };

            yield return new object[]
            {
                new Bill()
                {
                    Name = faker.Name.FirstName(),
                    Currency = faker.PickRandom<Currency>(),
                    Value = faker.Random.Double(1, 1000000),
                    Type = faker.PickRandom<BillType>(),
                    ExpenseDate = default,
                    Source = faker.Random.Word()
                },
                "Insira a data da despesa."
            };

            yield return new object[]
            {
                new Bill()
                {
                    Name = faker.Name.FirstName(),
                    Currency = faker.PickRandom<Currency>(),
                    Value = faker.Random.Double(1, 1000000),
                    Type = faker.PickRandom<BillType>(),
                    ExpenseDate = faker.Date.Future(1, DateTime.Now),
                    Source = null
                },
                "Insira a origem da despesa."
            };

            yield return new object[]
            {
                new Bill()
                {
                    Name = faker.Name.FirstName(),
                    Currency = faker.PickRandom<Currency>(),
                    Value = 1000001,
                    Type = faker.PickRandom<BillType>(),
                    ExpenseDate = faker.Date.Future(1, DateTime.Now),
                    Source = faker.Random.Word()
                },
                @"O valor da despesa não pode ser maior que R$ 1 milhão."
            };

            yield return new object[]
            {
                new Bill()
                {
                    Name = faker.Name.FirstName(),
                    Currency = faker.PickRandom<Currency>(),
                    Value = faker.Random.Double(1, 1000000),
                    Type = faker.PickRandom<BillType>(),
                    ExpenseDate = DateTime.Now.AddYears(-1),
                    Source = faker.Random.Word()
                },
                @"Despesas de mais de 1 ano atrás não podem ser adicionadas."
            };
        }
    }
}
