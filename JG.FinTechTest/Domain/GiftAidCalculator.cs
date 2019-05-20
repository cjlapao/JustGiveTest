using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JG.FinTechTest.Domain
{
    public class GiftAidCalculator : IGiftAidCalculator
    {
        private readonly IConfiguration _config;
        private readonly ILogger _log;
        private readonly double _taxRate;

        public GiftAidCalculator(IConfiguration config, ILogger<GiftAidCalculator> logger)
        {
            _config = config;
            _log = logger;
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
                _log.LogInformation("Calculation completed successfully");
                return donation * (_taxRate / (100 - _taxRate));
            }
            catch(Exception ex)
            {
                _log.LogError(ex, "There was an error when performing the calculation");
                throw;
            }
        }
    }
}
