using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using JG.FinTechTest.Domain;
using JG.FinTechTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSwag.Annotations;

namespace JG.FinTechTest.Controllers
{
    [Route("api/giftaid")]
    [ApiController]
    public class GiftAidController : ControllerBase
    {
        private readonly IGiftAidCalculator _calc;
        private readonly ILogger _log;

        public GiftAidController(IGiftAidCalculator calculator, ILogger<GiftAidController> logger)
        {
            _calc = calculator;
            _log = logger;
        }

        /// <summary>
        /// Calculates a Gift Aid based on the tax and the provided amount
        /// </summary>
        /// <param name="amount">Amount for calculation</param>
        /// <returns><see cref="GiftAidGetResultViewModel"/> Result</returns>
        /// <returns><see cref="ApiError"/> Result</returns>
        [HttpGet]
        [ProducesResponseType(typeof(GiftAidGetResultViewModel), 200)]     
        [ProducesResponseType(typeof(ApiError), 400)]
        public IActionResult Get([Required, FromQuery]double amount)
        {
            if (ModelState.IsValid)
            {
                if(amount < 2 || amount > 100_000)
                {
                    _log.LogInformation("Wrong amount provided on the query");
                    return BadRequest(new ApiError
                    {
                        Id = "10.1",
                        Description = "Please choose an amount between £2.00 and £100.000.00",
                        Message = "Invalid Amount"
                    });
                }

                _log.LogInformation("Gift aid calculated successfully");
                return new JsonResult(new GiftAidGetResultViewModel
                {
                    DonationAmount = amount,
                    GiftAidAmount = _calc.Calculate(amount)
                });
            }

            _log.LogInformation("Invalid amount provided");
            return BadRequest(new ApiError
            {
                Id = "10.2",
                Description = "Please provide a valid a donation amount to calculate  gift aid",
                Message = ModelState["amount"].Errors[0].ErrorMessage
            });
        }
    }
}
