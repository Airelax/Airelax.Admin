using System.Collections.Generic;
using Airelax.Infrastructure.ThirdPartyPayment.ECPay.Definitions;

namespace Airelax.Infrastructure.ThirdPartyPayment.ECPay.Request
{
    public class TokenRequestData
    {
        public string MerchantId { get; set; }
        public RememberCard RememberCard { get; set; }
        public PaymentUIType PaymentUIType { get; set; }
        public string ChoosePaymentList { get; set; }
        public OrderInfo OrderInfo { get; set; }
        public CardInfo CardInfo { get; set; }
        public ATMInfo ATMInfo { get; set; }
        public ConsumerInfo ConsumerInfo { get; set; }
    }

    public class OrderInfo
    {
        public string MerchantTradeNo { get; set; }
        public string MerchantTradeDate { get; set; }
        public int TotalAmount { get; set; }
        public string ReturnUrl { get; set; }
        public string TradeDesc { get; set; }
        public string ItemName { get; set; }
    }

    public class CardInfo
    {
        public string Redeem { get; set; }
        public int PeriodAmount { get; set; }
        public string PeriodType { get; set; }
        public int Frequency { get; set; }
        public int ExecTimes { get; set; }
        public string OrderResultUrl { get; set; }
        public string PeriodReturnUrl { get; set; }
        public string CreditInstallment { get; set; }
    }

    public class ATMInfo
    {
        public int ExpireDate { get; set; }
        public ATMBankCode ATMBankCode { get; set; }
    }

    public class ConsumerInfo
    {
        public string MerchantMemberId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public string CountryCode { get; set; }
    }
}