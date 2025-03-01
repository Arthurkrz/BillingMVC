using BillingMVC.Core.Contracts.Mapping;
using BillingMVC.Core.Entities;
using BillingMVC.Core.Enum;
using BillingMVC.Web.Mapping;
using BillingMVC.Web.Models;
using BillingMVC.Web.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace BillingMVC.Tests
{
    public class MappingTest
    {
        private readonly IMap _mapper;

        public MappingTest()
        {
            _mapper = new MappingProfile();
        }

        [Theory]
        [MemberData(nameof(GetValidObjects))]
        public void Map_MustSuccesfullyConvert<TSource, TTarget>
                    (TSource source, TTarget expectedTarget)
        {
            // Act
            var mappingProps = new Dictionary<string, string>
            {
                { "ValueString", "Value" },
                { "Value", "ValueString" },
                { "ValueRangeStart", "ValueStringRangeStart"},
                { "ValueRangeEnd", "ValueStringRangeEnd"},
                { "ValueStringRangeStart", "ValueRangeStart"},
                { "ValueStringRangeEnd", "ValueRangeEnd"},
            };

            var mapMethod = _mapper.GetType().GetMethod("Map").MakeGenericMethod(typeof(TSource), typeof(TTarget));
            object mapReturn = mapMethod.Invoke(_mapper, new object[] { source, mappingProps });

            // Assert
            PropertyInfo[] expectedTargetProps = expectedTarget.GetType().GetProperties();
            PropertyInfo[] mapReturnProps = mapReturn.GetType().GetProperties();
            
            foreach (var expectedProp in expectedTargetProps)
            {
                var mapReturnProp = mapReturnProps.FirstOrDefault(x => x.Name == expectedProp.Name);

                Assert.NotNull(mapReturnProp);

                var expectedValue = expectedProp.GetValue(expectedTarget);
                var actualValue = mapReturnProp.GetValue(mapReturn);

                Assert.Equal(expectedValue, actualValue);
            }
        }

        [Fact]
        public void Map_MustReturnException_WhenNullSource()
        {
            // Arrange
            Bill nullBill = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => 
            _mapper.Map<Bill, BillViewModel>(nullBill));
        }

        public static IEnumerable<object[]> GetValidObjects()
        {
            yield return new object[]
            {
                new Bill()
                {
                    Id = new Guid(),
                    Name = "Televisão",
                    Currency = Currency.Euro,
                    Value = 1000,
                    Type = BillType.House,
                    PurchaseDate = DateTime.Now.AddMonths(-1).Date,
                    Source = "Casas Bahia"
                },

                new BillViewModel()
                {
                    Id = new Guid(),
                    Name = "Televisão",
                    Currency = Web.Models.Enum.CurrencyVM.Euro,
                    ValueString = "1000.00",
                    Type = Web.Models.Enum.BillTypeVM.House,
                    PurchaseDate = DateTime.Now.AddMonths(-1).Date,
                    Source = "Casas Bahia"
                }
            };

            yield return new object[]
            {
                new BillViewModel()
                {   
                    Id = new Guid(),
                    Name = "Televisão",
                    Currency = Web.Models.Enum.CurrencyVM.Euro,
                    ValueString = "1000.00",
                    Type = Web.Models.Enum.BillTypeVM.House,
                    PurchaseDate = DateTime.Now.AddMonths(-1).Date,
                    Source = "Casas Bahia"
                },

                new Bill()
                {   
                    Id = new Guid(),
                    Name = "Televisão",
                    Currency = Currency.Euro,
                    Value = 1000,
                    Type = BillType.House,
                    PurchaseDate = DateTime.Now.AddMonths(-1).Date,
                    Source = "Casas Bahia"
                }
            };

            yield return new object[]
            {
                new BillFilter()
                {
                    NameContains = "Motocicleta",
                    SourceContains = "Yamaha",
                    ValueRangeStart = 9000,
                    ValueRangeEnd = 20000,
                    DateRangeStart = DateTime.Now.AddMonths(-6).Date,
                    DateRangeEnd = DateTime.Now.Date,
                    Currency = Currency.Real,
                    Type = BillType.Transport,
                    Month = PurchaseMonth.July
                },

                new BillFilterViewModel()
                {
                    NameContains = "Motocicleta",
                    SourceContains = "Yamaha",
                    ValueStringRangeStart = "9000.00",
                    ValueStringRangeEnd = "20000.00",
                    DateRangeStart = DateTime.Now.AddMonths(-6).Date,
                    DateRangeEnd = DateTime.Now.Date,
                    Currency = CurrencyVM.Real,
                    Type = Web.Models.Enum.BillTypeVM.Transport,
                    Month = PurchaseMonthVM.July,
                }
            };

            yield return new object[]
            {
                new BillFilterViewModel()
                {
                    NameContains = "Motocicleta",
                    SourceContains = "Yamaha",
                    ValueStringRangeStart = "9000.00",
                    ValueStringRangeEnd = "20000.00",
                    DateRangeStart = DateTime.Now.AddMonths(-6).Date,
                    DateRangeEnd = DateTime.Now.Date,
                    Currency = CurrencyVM.Real,
                    Type = Web.Models.Enum.BillTypeVM.Transport,
                    Month = PurchaseMonthVM.July,
                },

                new BillFilter()
                {
                    NameContains = "Motocicleta",
                    SourceContains = "Yamaha",
                    ValueRangeStart = 9000,
                    ValueRangeEnd = 20000,
                    DateRangeStart = DateTime.Now.AddMonths(-6).Date,
                    DateRangeEnd = DateTime.Now.Date,
                    Currency = Currency.Real,
                    Type = BillType.Transport,
                    Month = PurchaseMonth.July
                }
            };
        }
    }
}
