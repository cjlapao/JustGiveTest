using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JG.FinTechTest.Domain
{
    public interface IGiftAidCalculator
    {
        double Calculate(double donation);
    }
}
