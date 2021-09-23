namespace Airelax.Infrastructure.ThirdPartyPayment.ECPay.Request
{
    public class TransactRequestData
    {
        public string PlatformId { get; set; }
        public string MerchantId { get; set; }
        public string PayToken { get; set; }
        public string MerchantTradeNo { get; set; }
    }
}