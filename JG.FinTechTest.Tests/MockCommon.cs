using System;
using System.Collections.Generic;
using System.Text;
using JG.FinTechTest.Domain;
using Microsoft.Extensions.Configuration;
using Moq;

namespace JG.FinTechTest.Tests
{
    public static class MockCommon
    {
        public static IConfiguration GetIConfigService()
        {
            var configMoq = new Mock<IConfiguration>();
            configMoq.Setup(p => p["settings:taxRate"]).Returns("20.0");
            return configMoq.Object;
        }

        public static IGiftAidCalculator GetIGiftAidCarculatorService()
        {
            return new GiftAidCalculator(GetIConfigService());
        }
    }
}
