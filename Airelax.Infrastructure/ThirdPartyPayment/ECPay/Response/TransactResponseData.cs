using Airelax.Infrastructure.ThirdPartyPayment.ECPay.Request;

namespace Airelax.Infrastructure.ThirdPartyPayment.ECPay.Response
{
    public class TransactResponseData
    {
        public int RtnCode { get; set; }
        public string RtnMsg { get; set; }
        public string PlatformId { get; set; }
        public string MerchantId { get; set; }
        public ResponseOrderInfo OrderInfo { get; set; }
    }

    public class ResponseOrderInfo
    {
        public string MerchantTradeNo { get; set; }
        public string TradeNo { get; set; }
        public int TradeAmt { get; set; }
        public string TradeDate { get; set; }
        public string PaymentType { get; set; }
        public string PaymentDate { get; set; }
        public decimal ChargeFee { get; set; }
        public string TradeStatus { get; set; }
    }
}