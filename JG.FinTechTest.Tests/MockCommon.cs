using System;
using System.Collections.Generic;
using System.Text;
using JG.FinTechTest.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;

namespace JG.FinTechTest.Tests
{
    /// <summary>
    /// This is used as a helper on our tests
    /// </summary>
    public static class MockCommon
    {
        /// <summary>
        /// Get's the Mock configuration service
        /// </summary>
        /// <returns><see cref="IConfiguration"/></returns>
        public static IConfiguration GetIConfigService()
        {
            var configMoq = new Mock<IConfiguration>();
            configMoq.Setup(p => p["settings:taxRate"]).Returns("20.0");
            return configMoq.Object;
        }

        /// <summary>
        /// Get's the mocked GiftAidCalculatorService
        /// </summary>
        /// <returns><see cref="IGiftAidCalculator"/></returns>
        public static IGiftAidCalculator GetIGiftAidCalculatorService()
        {
            return new GiftAidCalculator(GetIConfigService(), GetLoggerService<GiftAidCalculator>());
        }

        /// <summary>
        /// Get's the mocked LoggerService
        /// </summary>
        /// <typeparam name="T">Class to log</typeparam>
        /// <returns><see cref="ILogger"/></returns>
        public static ILogger<T> GetLoggerService<T>()
            where T : class
        {
            return Mock.Of<ILogger<T>>();
        }
    }
}
