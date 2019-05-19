using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JG.FinTechTest.Domain
{
    public class GiftAidCalculator : IGiftAidCalculator
    {
        private readonly IConfiguration _config;
        private readonly double _taxRate;

        public GiftAidCalculator(IConfiguration config)
        {
            _config = config;
            if(! double.TryParse(_config["settings:taxRate"], out _taxRate))
            {
                throw new ArgumentNullException("taxRate", "Tax Rate was not found in the config file");
            }
        }

        public double Calculate(double donation)
        {
            try
            {
                if(donation < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(donation), "Donation cannot be negative");
                }

                return donation * (_taxRate / (100 - _taxRate));
            }
            catch
            {
                throw;
            }
        }
    }
}
