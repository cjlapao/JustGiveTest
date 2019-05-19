using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Threading.Tasks;
using JG.FinTechTest.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace JG.FinTechTest.Tests
{
    public class GiftAidControllerTests
    {
        [Theory]
        [InlineData(2.0, 0.5)]
        [InlineData(-3.4, 0)]
        [InlineData(200.0, 50.0)]
        [InlineData(100.0, 25.0)]
        public void GetGiftAidTest(double amount, object expectedResult)
        {
            var giftAidController = new GiftAidController(MockCommon.GetIGiftAidCarculatorService());

            var result = giftAidController.Get(amount);

            if (amount > 0)
            {
                _ = Assert.IsType<JsonResult>(result);
                dynamic jsonResult = (result as JsonResult)?.Value;
                Assert.Equal(expectedResult, jsonResult.GiftAidAmount ?? 0);
            }
            else
            {
                _ = Assert.IsType<BadRequestObjectResult>(result);
            }
        }
    }
}
