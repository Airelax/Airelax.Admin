using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Airelax.Admin.Defines;

namespace Airelax.Admin
{
    public class IncomeInput
    {
        /// <summary>
        /// 區間之起始時間
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 區間之結束時間
        /// </summary>
        public DateTime EndDate { get; set; }
        public DateType DateType { get; set; }
        public int Page { get; set; }
    }
}
