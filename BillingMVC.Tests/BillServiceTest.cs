using BillingMVC.Core.Contracts.Repositories;
using BillingMVC.Core.Entities;
using BillingMVC.Core.Enum;
using BillingMVC.Core.Validators;
using BillingMVC.Data.Repositories;
using BillingMVC.Service;
using Bogus;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace BillingMVC.Tests
{
    public class BillServiceTest
    {
        private readonly BillService _sut;
        private readonly Faker _faker;
        private IValidator<Bill> _validator;
        private readonly Mock<IBillRepository> _mockRepository;

        public BillServiceTest()
        {
            _validator = new BillValidator();
            _faker = new Faker();
            _mockRepository = new Mock<IBillRepository>();
            _sut = new BillService(_mockRepository.Object, _validator);
        }

        [Fact]
        public void CreateBill_DeveAdicionarComSucesso_QuandoObjetoValido()
        {
            // Arrange
            Bill bill = new Bill()
            {
                Name = _faker.Name.FirstName(),
                Currency = _faker.PickRandom<Currency>(),
                Value = _faker.Random.Double(1, 1000000),
                Type = _faker.PickRandom<BillType>(),
                PurchaseDate = _faker.Date.Future(),
                Source = _faker.Random.Word(),
            };

            // Act
            _sut.CreateBill(bill);

            // Assert
            _mockRepository.Verify(x => x.Add(bill), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetInvalidBills))]
        public void CreateBill_DeveRetornarErro_QuandoPropriedadeInvalida
                    (Bill bill, string errorMessage)
        {
            // Act & Assert
            var result = Assert.Throws<InvalidOperationException>(() => 
                                                 _sut.CreateBill(bill));

            string message = result.Message.Replace("\r\n", string.Empty);
            Assert.Equal(message, errorMessage);
        }

        [Fact]
        public void CreateBill_DeveRetornarErro_QuandoObjetoNulo()
        {
            // Arrange
            Bill bill = null;

            // Act & Assert
            Assert.Throws<Exception>(() => 
                   _sut.CreateBill(bill));
        }

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
                    PurchaseDate = faker.Date.Future(1, DateTime.Now),
                    Source = faker.Random.Word(),
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
                    PurchaseDate = faker.Date.Future(1, DateTime.Now),
                    Source = faker.Random.Word(),
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
                    PurchaseDate = faker.Date.Future(1, DateTime.Now),
                    Source = faker.Random.Word(),
                },

                "Especifique o valor da conta."
            };

            yield return new object[]
            {
                new Bill()
                {
                    Name = faker.Name.FirstName(),
                    Currency = faker.PickRandom<Currency>(),
                    Value = faker.Random.Double(1, 1000000),
                    Type = null,
                    PurchaseDate = faker.Date.Future(1, DateTime.Now),
                    Source = faker.Random.Word(),
                },

                "Especifique a categoria da conta."
            };

            yield return new object[]
            {
                new Bill()
                {
                    Name = faker.Name.FirstName(),
                    Currency = faker.PickRandom<Currency>(),
                    Value = faker.Random.Double(1, 1000000),
                    Type = faker.PickRandom<BillType>(),
                    PurchaseDate = default,
                    Source = faker.Random.Word(),
                },

                "Insira uma data de " +
                "vencimento da conta."
            };

            yield return new object[]
            {
                new Bill()
                {
                    Name = faker.Name.FirstName(),
                    Currency = faker.PickRandom<Currency>(),
                    Value = faker.Random.Double(1, 1000000),
                    Type = faker.PickRandom<BillType>(),
                    PurchaseDate = faker.Date.Future(1, DateTime.Now),
                    Source = null,
                },

                "Insira a origem da conta."
            };

            yield return new object[]
            {
                new Bill()
                {
                    Name = faker.Name.FirstName(),
                    Currency = faker.PickRandom<Currency>(),
                    Value = faker.Random.Double(1, 1000000),
                    Type = faker.PickRandom<BillType>(),
                    PurchaseDate = faker.Date.Future(1, DateTime.Now),
                    Source = faker.Random.Word(),
                },

                "Especifique se a conta " +
                "foi paga ou não."
            };

            yield return new object[]
            {
                new Bill()
                {
                    Name = faker.Name.FirstName(),
                    Currency = faker.PickRandom<Currency>(),
                    Value = 1000001,
                    Type = faker.PickRandom<BillType>(),
                    PurchaseDate = faker.Date.Future(1, DateTime.Now),
                    Source = faker.Random.Word(),
                },

                "O valor da conta não pode " +
                "ser maior que R$ 1 milhão."
            };

            yield return new object[]
            {
                new Bill()
                {
                    Name = faker.Name.FirstName(),
                    Currency = faker.PickRandom<Currency>(),
                    Value = faker.Random.Double(1, 1000000),
                    Type = faker.PickRandom<BillType>(),
                    PurchaseDate = DateTime.Now.AddMonths(-6),
                    Source = faker.Random.Word(),
                },

                "Contas não pagas vencidas há " +
                "mais de 6 meses não podem " +
                "ser adicionadas."
            };


            yield return new object[]
            {
                new Bill()
                {
                    Name = faker.Name.FirstName(),
                    Currency = faker.PickRandom<Currency>(),
                    Value = faker.Random.Double(1, 1000000),
                    Type = faker.PickRandom<BillType>(),
                    PurchaseDate = DateTime.Now.AddYears(-1),
                    Source = faker.Random.Word(),
                },

                "Contas vencidas a mais " +
                "de 1 ano não podem " +
                "ser adicionadas."
            };
        }
    }
}
