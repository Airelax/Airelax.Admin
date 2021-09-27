using System;
using System.Collections.Generic;

namespace Airelax.Admin
{
    public interface IIncomeService
    {
        Dictionary<string, decimal> ConvertToDictionary(IncomeInput incomeInput);
    }
}