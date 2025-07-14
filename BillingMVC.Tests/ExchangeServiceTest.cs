using BillingMVC.Core.Contracts.ExternalServices;
using BillingMVC.Core.Contracts.Services;
using BillingMVC.Core.DTOS;
using BillingMVC.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace BillingMVC.Tests
{
    public class ExchangeServiceTest
    {
        private readonly Mock<IExchangeHandler> _exchangeHandlerMock;
        private readonly Mock<IMemoryCacheService> _memoryCacheServiceMock;
        private readonly ExchangeService _sut;

        public ExchangeServiceTest()
        {
            _exchangeHandlerMock = new Mock<IExchangeHandler>();
            _memoryCacheServiceMock = new Mock<IMemoryCacheService>();

            _sut = new ExchangeService(_exchangeHandlerMock.Object, 
                                       _memoryCacheServiceMock.Object);
        }

        [Fact]
        public async Task ExchangeService_MustReturnExchangeRate_WhenCacheExists()
        {
            // Arrange
            var exchangeResult = new ExchangeResultDTO
            {
                Rates = new Dictionary<string, double>
                {
                    { "BRL", 6.060268 }
                }
            };

            _exchangeHandlerMock.Setup(x => x.GetExchangeOfTheDay())
                                             .ReturnsAsync(exchangeResult);

            _memoryCacheServiceMock.Setup(x => x.GetOrCreateAsync(
                                                 It.IsAny<string>(),
                                                 It.IsAny<Func<Task<ExchangeResultDTO>>>()))
                                                .ReturnsAsync(exchangeResult);
            // Act & Assert
            var result = await _sut.GetExchangeAsync();

            _memoryCacheServiceMock.Verify(x => x.GetOrCreateAsync(
                                                  "exchange", 
                                                  It.IsAny<Func<Task<ExchangeResultDTO>>>()), 
                                                  Times.Once);

            Assert.Equal(exchangeResult.Rates, result);
        }
    }
}
