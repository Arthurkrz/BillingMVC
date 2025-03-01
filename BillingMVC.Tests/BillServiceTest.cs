using BillingMVC.Core.Contracts.Repositories;
using BillingMVC.Core.Entities;
using BillingMVC.Core.Enum;
using BillingMVC.Core.Validators;
using BillingMVC.Data.Repositories;
using BillingMVC.Service;
using BillingMVC.Service.PredicateBuilder;
using Bogus;
using FluentValidation;
using FluentValidation.Results;
using LinqKit;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace BillingMVC.Tests
{
    public class BillServiceTest
    {
        private readonly BillService _sut;
        private readonly Faker _faker;
        private IValidator<Bill> _validator;
        private IValidator<BillFilter> _validatorFilter;
        private readonly Mock<IBillRepository> _mockRepository;

        public BillServiceTest()
        {
            _validator = new BillValidator();
            _validatorFilter = new BillFilterValidator();
            _faker = new Faker();
            _mockRepository = new Mock<IBillRepository>();
            _sut = new BillService(_mockRepository.Object, _validator, _validatorFilter);
        }

        [Fact]
        public async Task CreateBill_MustAddSuccesfully()
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
            await _sut.CreateBill(bill);

            // Assert
            _mockRepository.Verify(x => x.Add(bill), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetInvalidBills))]
        public async Task CreateBill_MustReturnError_WhenInvalidProperty
                    (Bill bill, string errorMessage)
        {
            // Act & Assert
            var result = await _sut.CreateBill(bill);

            Assert.False(result.Success);
            Assert.Contains(errorMessage, result.Errors);
        }

        [Fact]
        public async Task CreateBill_MustReturnError_WhenNullObject()
        {
            // Arrange
            Bill bill = null;

            // Act
            var result = await _sut.CreateBill(bill);

            // Act & Assert
            Assert.False(result.Success);
            Assert.Contains("A despesa não pode ser nula.", result.Errors);
        }

        [Fact]
        public async Task List_MustCallRepository()
        {
            // Act & Assert
            var result = await _sut.List();
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetBillsWithFilter_MustGetSuccesfully()
        {
            BillFilter billFilter = new BillFilter
            {
                NameContains = "Arroz",
                SourceContains = "Jacomar",
                ValueRangeStart = 25,
                ValueRangeEnd = 150,
                DateRangeStart = DateTime.Now.AddMonths(-1),
                DateRangeEnd = DateTime.Now,
                Currency = Currency.Real,
                Type = BillType.Food,
                Month = PurchaseMonth.February
            };

            var filterExpression = BillPredicateBuilder.Build(billFilter);
            Assert.NotNull(filterExpression);

            var result = await _sut.GetBillsWithFilter(billFilter);
            _mockRepository.Verify(x => x.GetBillsWithFilter(It.IsAny<Expression<Func<Bill, bool>>>()), Times.AtLeastOnce);
            Assert.True(result.Success);
        }

        [Theory]
        [MemberData(nameof(GetInvalidFilters))]
        public async Task GetBillsWithFilter_MustReturnError_WhenInvalidFilter
                          (BillFilter billFilter, string errorMessage)
        {
            // Act & Assert
            var result = await _sut.GetBillsWithFilter(billFilter);

            Assert.False(result.Success);
            Assert.Contains(errorMessage, result.Errors);
        }

        [Fact]
        public async Task UpdateBill_MustUpdateSuccesfully()
        {
            // Arrange
            Bill bill = new Bill
            {
                Id = new Guid(),
                Name = "Almoço",
                Currency = Currency.Euro,
                Value = 1000,
                Type = BillType.Food,
                PurchaseDate = DateTime.Now,
                Source = "Batel Grill"
            };

            _mockRepository.Setup(x => x.GetById(bill.Id)).ReturnsAsync(bill);

            var result = await _sut.UpdateBill(bill);

            Assert.True(result.Success);
            _mockRepository.Verify(x => x.Update(bill), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetInvalidBills))]
        public async Task UpdateBill_MustReturnError_WhenInvalidProperty(Bill bill, string errorMessage)
        {
            _mockRepository.Setup(x => x.GetById(bill.Id))
                           .ReturnsAsync(bill);

            var result = await _sut.UpdateBill(bill);

            Assert.False(result.Success);
            Assert.Contains(errorMessage, result.Errors);
        }

        [Fact]
        public async Task UpdateBill_MustReturnError_WhenBillNotFound()
        {
            // Arrange
            Bill bill = new Bill
            {
                Id = new Guid(),
                Name = "Almoço",
                Currency = Currency.Euro,
                Value = 1000,
                Type = BillType.Food,
                PurchaseDate = DateTime.Now,
                Source = "Batel Grill"
            };

            // Act
            var result = await _sut.UpdateBill(bill);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("A despesa não foi encontrada.", result.Errors);
        }

        [Fact]
        public async Task DeleteBill_MustDeleteSuccesfully()
        {
            // Arrange
            Guid id = new Guid();
            Bill bill = new Bill();

            _mockRepository.Setup(x => x.GetById(id)).ReturnsAsync(bill);

            // Act
            var result = await _sut.DeleteBill(id);

            // Assert
            _mockRepository.Verify(x => x.Delete(bill), Times.Once);
            Assert.True(result.Success);
        }

        [Fact]
        public async Task DeleteBill_MustReturnError_WhenObjectNotFound()
        {
            // Act & Assert
            var result = await _sut.DeleteBill(new Guid());

            Assert.False(result.Success);
            Assert.Contains("ID não corresponde a nenhuma despesa.", result.Errors);
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
                    PurchaseDate = faker.Date.Future(1, DateTime.Now),
                    Source = faker.Random.Word(),
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
                    PurchaseDate = default,
                    Source = faker.Random.Word(),
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
                    PurchaseDate = faker.Date.Future(1, DateTime.Now),
                    Source = null,
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
                    PurchaseDate = faker.Date.Future(1, DateTime.Now),
                    Source = faker.Random.Word(),
                },

                "O valor da despesa não pode " +
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
                    PurchaseDate = DateTime.Now.AddYears(-1),
                    Source = faker.Random.Word(),
                },

                "Despesas de mais " +
                "de 1 ano atrás não podem " +
                "ser adicionadas."
            };
        }

        public static IEnumerable<object[]> GetInvalidFilters()
        {
            Faker faker = new Faker();

            yield return new object[]
            {
                new BillFilter()
                {
                    DateRangeStart = DateTime.Now
                                             .AddYears(-1),
                },

                "Não é possível listar " +
                "despesas de mais de " +
                "1 ano atrás."
            };

            yield return new object[]
            {
                new BillFilter()
                {
                    DateRangeStart = DateTime.Now,
                    DateRangeEnd = DateTime.Now
                                           .AddDays(-1),
                },

                "O intervalo inicial de " +
                "data da despesa não " +
                "pode ser maior que " +
                "o intervalo final."
            };

            yield return new object[]
            {
                new BillFilter()
                {
                    ValueRangeStart = 2,
                    ValueRangeEnd = 1,
                },

                "O intervalo inicial de " +
                "valor da despesa não " +
                "pode ser maior que " +
                "o intervalo final."
            };

            yield return new object[]
            {
                new BillFilter()
                {
                    ValueRangeStart = -1,
                },

                "O intervalo inicial de " +
                "valor da despesa não " +
                "pode ser menor " +
                "que zero."
            };

            yield return new object[]
            {
                new BillFilter()
                {
                    ValueRangeEnd = 1000001,
                },

                "O intervalo final de " +
                "valor da despesa não " +
                "pode ser maior que " +
                "1 milhão."
            };
        }
    }
}