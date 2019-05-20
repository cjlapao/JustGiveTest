using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JG.FinTechTest.Models;
using JG.FinTechTest.Persistence;
using Microsoft.Extensions.Logging;

namespace JG.FinTechTest.Controllers
{
    [Route("api/giftaiddeclaration")]
    [ApiController]
    public class GiftAidDeclarationsController : ControllerBase
    {
        private readonly JGFinTechTestContext _context;
        private readonly ILogger _log;

        public GiftAidDeclarationsController(JGFinTechTestContext context, ILogger logger)
        {
            _context = context;
            _log = logger;
        }

        /// <summary>
        /// Creates a GiftAidDeclaration in the database
        /// </summary>
        /// <param name="giftAidDeclaration"><see cref="GiftAidDeclaration"/></param>
        /// <returns><see cref="GiftAidDeclaration"/></returns>
        [HttpPost]
        [ProducesResponseType(typeof(GiftAidDeclaration), 201)]
        [ProducesResponseType(typeof(ApiError[]), 400)]
        public async Task<ActionResult<GiftAidDeclaration>> Post(GiftAidDeclarationModel giftAidDeclaration)
        {
            List<ApiError> apiErrors = new List<ApiError>();
            if (ModelState.IsValid)
            {
                _log.LogInformation("Starting GiftAidDeclaration");
                GiftAidDeclaration dec = new GiftAidDeclaration
                {
                    Name = giftAidDeclaration.Name,
                    DonationAmount = giftAidDeclaration.DonationAmount,
                    PostCode = giftAidDeclaration.PostCode
                };
                try
                {
                    _context.GiftAidDeclaration.Add(dec);
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                    _log.LogInformation("GiftAidDeclaration saved successfully in the database");
                }
                catch (Exception ex)
                {
                    ApiError apiError = new ApiError
                    {
                        Id = "11.4",
                        Description = "There was a server error, please contact support",
                        Message = "System error"
                    };

                    apiErrors.Add(apiError);

                    _log.LogError(ex, "There was an error saving the declaration to the database");
                    return BadRequest(apiErrors);
                }
                return new CreatedResult("PostGiftAidDeclaration", dec);
            }

            foreach(var error in ModelState)
            {
                ApiError apiError = new ApiError();
                switch (error.Key.ToLowerInvariant())
                {
                    case "postcode":
                        apiError.Id = "11.1";
                        apiError.Description = "Failed to validate postcode, format unknown";
                        apiError.Message = "Invalid Postcode";
                        break;
                    case "donationamount":
                        apiError.Id = "11.2";
                        apiError.Description = "Please choose an amount between £2.00 and £100.000.00";
                        apiError.Message = "Invalid Donation Amount";
                        break;
                    case "name":
                        apiError.Id = "11.3";
                        apiError.Description = "Name cannot be empty, please choose a valid name";
                        apiError.Message = "Invalid Name";
                        break;
                }

                apiErrors.Add(apiError);
            }

            _log.LogError("There was an error in the model provided, cannot save data to database");
            return BadRequest(apiErrors);
        }
    }
}
