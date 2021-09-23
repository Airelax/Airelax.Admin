namespace Airelax.Infrastructure.ThirdPartyPayment.ECPay.Definitions.Options
{
    public class ECPaySetting
    {
        public string AesKey { get; set; }
        public string AesIV { get; set; }
        public string BaseUrl { get; set; }
        public string Revision { get; set; }
        public string MerchantId { get; set; }
        public string TaxId { get; set; }
        public Apis Apis { get; set; }
    }
}