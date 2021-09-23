namespace Airelax.Infrastructure.ThirdPartyPayment.ECPay.Definitions
{
    /// <summary>
    /// 欲使用的付款方式
    /// </summary>
    public enum ChoosePayment
    {
        /// <summary>
        /// 全部付款方式
        /// </summary>
        AllPayment = 0,

        /// <summary>
        /// 信用卡一次付清
        /// </summary>
        CreditCardPayAllAtOnce = 1,

        /// <summary>
        /// 信用卡分期付款
        /// </summary>
        CreditCardInstallment = 2,

        /// <summary>
        /// ATM
        /// </summary>
        ATM = 3,

        /// <summary>
        /// 超商代碼
        /// </summary>
        CVS = 4,

        /// <summary>
        /// 超商條碼
        /// </summary>
        BarCode = 5
    }
}