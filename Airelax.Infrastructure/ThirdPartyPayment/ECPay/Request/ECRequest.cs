using System.Net.Http;

namespace Airelax.Infrastructure.ThirdPartyPayment.ECPay.Request
{
    public class ECRequest
    {
        public string MerchantId { get; set; }
        public ECRequestHeader RqHeader { get; set; }
        public string Data { get; set; }
    }
}