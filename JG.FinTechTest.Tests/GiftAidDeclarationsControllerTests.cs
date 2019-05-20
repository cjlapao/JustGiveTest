using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JG.FinTechTest.Controllers;
using JG.FinTechTest.Models;
using JG.FinTechTest.Persistence;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace JG.FinTechTest.Tests
{
    public class GiftAidDeclarationsControllerTests
    {
        // Creating a static set of test's to use in our theory
        public static IEnumerable<object[]> GiftAidDeclarationData
        {
            get
            {
                return new[]
                {
                    new object[]
                    {
                        new GiftAidDeclarationModel
                        {
                            Name = "Steve",
                            DonationAmount = 20,
                            PostCode = "AA10-9AR"
                        },
                        true,
                        string.Empty
                    },
                    new object[]
                    {
                        new GiftAidDeclarationModel
                        {
                            Name = "John",
                            DonationAmount = 20,
                            PostCode = "AA-9AR"
                        },
                        false,
                        "PostCode"
                    },
                    new object[]
                    {
                        new GiftAidDeclarationModel
                        {
                            Name = "David",
                            DonationAmount = 1,
                            PostCode = "AA11W-9AR"
                        },
                        false,
                        "DonationAmount"
                    },
                    new object[]
                    {
                        new GiftAidDeclarationModel
                        {
                            Name = "Andrew",
                            DonationAmount = 101_000,
                            PostCode = "AA10-9AR"
                        },
                        false,
                        "DonationAmount"
                    },
                    new object[]
                    {
                        new GiftAidDeclarationModel
                        {
                            Name = null,
                            DonationAmount = 20,
                            PostCode = "AA10-9AR"
                        },
                        false,
                        "Name"
                    }
                };
            }
        }

        [Theory]
        [MemberData(nameof(GiftAidDeclarationData))]
        public async Task PostGiftAidDeclarationTest(GiftAidDeclarationModel model, bool expectedResult, string apiError)
        {
            // Mocking the database to a memory database
            JGFinTechTestContext context = MemoryRepository.Create("PostGiftAidDeclarationTest").Context;

            // Creating the controller
            var giftAidDeclarationController = new GiftAidDeclarationsController(context, MockCommon.GetLoggerService<GiftAidDeclarationsController>());

            // Testing the endpoint with the data model
            var result = await giftAidDeclarationController.Post(model).ConfigureAwait(false);

            // We need to perform the asp.net core validation filter manually as this is done by the framework
            // Creating the container for the validation results
            var validationResults = new List<ValidationResult>();

            // Creating the validation context and trying to validate the object
            var validationContext = new ValidationContext(model);
            Validator.TryValidateObject(model, validationContext, validationResults, true);

            // If the expected result is a Positive and the validation did not detect errors we will check if the object
            // was stored in the memory database, else we will check if the validation error is what we expected
            if (expectedResult && validationResults.Count == 0)
            {
                _ = Assert.IsType<ActionResult<GiftAidDeclaration>>(result);
                var dbResult = context.GiftAidDeclaration.FirstOrDefault(x => x.Name.Equals(model.Name) && x.PostCode.Equals(model.PostCode));
                Assert.NotNull(dbResult);
            }
            else
            {
                Assert.Equal(validationResults[0].MemberNames.FirstOrDefault()?.ToLowerInvariant(), apiError.ToLowerInvariant());
            }
        }
    }
}
