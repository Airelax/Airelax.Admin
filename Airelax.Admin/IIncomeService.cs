using System;
using System.Collections.Generic;

namespace Airelax.Admin
{
    public interface IIncomeService
    {
        Dictionary<string, long> GetIncome(IncomeInput incomeInput);
    }
}