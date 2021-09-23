using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airelax.Infrastructure.ThirdPartyPayment.ECPay.Request
{
    public class CreateTokenInput
    {
        public string MerchantTradeNo { get; set; }
        public int TotalAmount { get; set; }
        public string TradeDesc { get; set; }
        public string ItemName { get; set; }
        public string Phone { get; set; } = "886912345678";
        public string Name { get; set; }
        public string Email { get; set; }
        public string MerchantMemberId { get; set; }
    }
}
