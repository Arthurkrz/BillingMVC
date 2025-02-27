using BillingMVC.Core.Contracts.Mapping;
using BillingMVC.Core.Entities;
using BillingMVC.Core.Enum;
using BillingMVC.Web.Mapping;
using BillingMVC.Web.Models;
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
        public void Map_DeveConverterComSucesso_ComObjetosValidos
                    (object source, object expectedTarget)
        {
            // Arrange
            PropertyInfo[] expectedTargetProps = expectedTarget
                                     .GetType().GetProperties();

            // Act
            object mapReturn = _mapper.Map<object, object>(source);

            // Assert
            PropertyInfo[] mapReturnProps = mapReturn
                           .GetType().GetProperties();

            foreach (var prop in expectedTargetProps)
            {
                var mapReturnProp = mapReturnProps
                                   .FirstOrDefault
                                   (x => x.Name == prop.Name);

                Assert.Equal(prop, mapReturnProp);
            }
        }

        [Fact]
        public void Map_DeveRetornarException_QuandoSourceNulo()
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
                    Name = "Televisão",
                    Currency = Currency.Euro,
                    Value = 1000,
                    Type = BillType.House,
                    PurchaseDate = DateTime.Now.AddMonths(-1),
                    Source = "Casas Bahia"
                },

                new BillViewModel()
                {
                    Name = "Televisão",
                    Currency = Web.Models.Enum.CurrencyVM.Euro,
                    ValueString = "1000.00",
                    Type = Web.Models.Enum.BillTypeVM.House,
                    PurchaseDate = DateTime.Now.AddMonths(-1),
                    Source = "Casas Bahia"
                }
            };

            yield return new object[]
            {
                new BillViewModel()
                {
                    Name = "Televisão",
                    Currency = Web.Models.Enum.CurrencyVM.Euro,
                    ValueString = "1000.00",
                    Type = Web.Models.Enum.BillTypeVM.House,
                    PurchaseDate = DateTime.Now.AddMonths(-1),
                    Source = "Casas Bahia"
                },

                new Bill()
                {
                    Name = "Televisão",
                    Currency = Currency.Euro,
                    Value = 1000,
                    Type = BillType.House,
                    PurchaseDate = DateTime.Now.AddMonths(-1),
                    Source = "Casas Bahia"
                }
            };
        }
    }
}
