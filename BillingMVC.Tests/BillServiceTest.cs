using BillingMVC.Core.Contracts.Repositories;
using BillingMVC.Core.Entities;
using BillingMVC.Core.Enum;
using BillingMVC.Core.Validators;
using BillingMVC.Service;
using BillingMVC.Service.PredicateBuilder;
using BillingMVC.Tests.ObjectGenerators;
using Bogus;
using FluentValidation;
using Moq;
using System;
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
                ExpenseDate = _faker.Date.Future(),
                Source = _faker.Random.Word(),
            };

            // Act & Assert
            await _sut.CreateBill(bill);
            _mockRepository.Verify(x => x.Add(bill), Times.Once);
        }

        [Theory]
        [MemberData(nameof(BillData.GetInvalidBills), MemberType = typeof(BillData))]
        public async Task CreateBill_MustReturnError_WhenInvalidProperty(Bill bill, string errorMessage)
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

            // Act & Assert
            var result = await _sut.CreateBill(bill);

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
            // Arrange
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

            // Act
            var filterExpression = BillPredicateBuilder.Build(billFilter);
            var result = await _sut.GetBillsWithFilter(billFilter);

            // Assert
            Assert.NotNull(filterExpression);
            Assert.True(result.Success);

            _mockRepository.Verify(x => x.GetBillsWithFilter(
                It.IsAny<Expression<Func<Bill, bool>>>()), Times.AtLeastOnce);
        }

        [Theory]
        [MemberData(nameof(BillFilterData.GetInvalidFilters), MemberType = typeof(BillFilterData))]
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
                ExpenseDate = DateTime.Now,
                Source = "Batel Grill"
            };

            _mockRepository.Setup(x => x.GetById(bill.Id)).ReturnsAsync(bill);

            // Act & Assert
            var result = await _sut.UpdateBill(bill);

            Assert.True(result.Success);
            _mockRepository.Verify(x => x.Update(bill), Times.Once);
        }

        [Theory]
        [MemberData(nameof(BillData.GetInvalidBills), MemberType = typeof(BillData))]
        public async Task UpdateBill_MustReturnError_WhenInvalidProperty(Bill bill, string errorMessage)
        {
            // Act & Assert
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
                ExpenseDate = DateTime.Now,
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
    }
}