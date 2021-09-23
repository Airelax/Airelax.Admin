namespace Airelax.Infrastructure.ThirdPartyPayment.ECPay.Definitions
{
   public struct ECDefinition
   {
      public struct Token
      {
         /// <summary>
         /// 使用信用卡紅利
         /// </summary>
         public struct Redeem
         {
            /// <summary>
            /// 不使用
            /// </summary>
            public const string NotUse = "0";
            /// <summary>
            /// 使用
            /// </summary>
            public const string Use = "1";
         }
         
         /// <summary>
         /// 定期定額週期種類
         /// </summary>
         public struct PeriodType
         {
            public const string Day = "D";
            public const string Month = "M";
            public const string Year = "Y";
         }
      }
   }
}