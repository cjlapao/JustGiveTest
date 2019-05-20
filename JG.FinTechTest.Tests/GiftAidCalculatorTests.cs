using System;
using JG.FinTechTest.Domain;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace JG.FinTechTest.Tests
{
    public class GiftAidCalculatorTests
    {
        [Theory]
        [InlineData(2.0, 0.5)]
        [InlineData(-3.4, 0)]
        [InlineData(200, 50)]
        [InlineData(100, 25)]
        public void Calculate(double amount, double expectedValue)
        {
            GiftAidCalculator calculator = new GiftAidCalculator(MockCommon.GetIConfigService(), MockCommon.GetLoggerService<GiftAidCalculator>());
            if(amount > 0)
            {
                double result = calculator.Calculate(amount);
                Assert.Equal(expectedValue, result);
            }
            else
            {
                _ = Assert.Throws<ArgumentOutOfRangeException>(() => calculator.Calculate(amount));
            }
        }
    }
}