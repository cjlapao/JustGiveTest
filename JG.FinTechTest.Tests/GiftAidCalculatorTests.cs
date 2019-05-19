using System;
using JG.FinTechTest.Domain;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace Tests
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
            var configMoq = new Mock<IConfiguration>();
            configMoq.Setup(p => p["settings:taxRate"]).Returns("20.0");
            GiftAidCalculator calculator = new GiftAidCalculator(configMoq.Object);
            if(amount > 0)
            {
                double result = calculator.Calculate(amount);
                Assert.Equal(expectedValue, result);
            }
            else
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => calculator.Calculate(amount));
            }
        }
    }
}