using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using JG.FinTechTest.Domain;
using JG.FinTechTest.Models;
using Microsoft.AspNetCore.Mvc;

namespace JG.FinTechTest.Controllers
{
    [Route("api/giftaid")]
    [ApiController]
    public class GiftAidController : ControllerBase
    {
        private readonly IGiftAidCalculator _calc;

        public GiftAidController(IGiftAidCalculator calculator)
        {
            _calc = calculator;
        }

        [HttpGet]
        public IActionResult Get([Required, FromQuery]double amount)
        {
            if (ModelState.IsValid)
            {
                if(amount < 2 || amount > 100_000)
                {
                    return BadRequest(new ApiError
                    {
                        Id = "10.1",
                        Description = "Please choose an amount between £2.00 and £100.000.00",
                        Message = "Invalid Amount"
                    });
                }

                return new JsonResult(new GiftAidGetResultViewModel
                {
                    DonationAmount = amount,
                    GiftAidAmount = _calc.Calculate(amount)
                });
            }

            return BadRequest(new ApiError
            {
                Id = "10.2",
                Description = "Please provide a valid a donation amount to calculate  gift aid",
                Message = ModelState["amount"].Errors[0].ErrorMessage
            });
        }
    }
}
