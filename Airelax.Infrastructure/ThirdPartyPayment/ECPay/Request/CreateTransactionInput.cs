using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airelax.Infrastructure.ThirdPartyPayment.ECPay.Request
{
    public class CreateTransactionInput
    {
        public string Token { get; set; }
        public string MerchantTradeNo { get; set; }
    }
}
