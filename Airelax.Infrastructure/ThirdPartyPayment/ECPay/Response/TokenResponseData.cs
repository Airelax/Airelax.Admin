namespace Airelax.Infrastructure.ThirdPartyPayment.ECPay.Response
{
    public class TokenResponseData
    {
        public int RtnCode { get; set; }
        public string RtnMsg { get; set; }
        public string PlatformId { get; set; }
        public string MerchantId { get; set; }
        public string Token { get; set; }
        public string TokenExpireDate { get; set; }
    }
}