namespace Airelax.Infrastructure.ThirdPartyPayment.ECPay.Response
{
    public class TokenResponse
    {
        public string MerchantId { get; set; }
        public ECResponseHeader RpHeader { get; set; }
        public int TransCode { get; set; }
        public string TransMsg { get; set; }
        public string Data { get; set; }
    }
}