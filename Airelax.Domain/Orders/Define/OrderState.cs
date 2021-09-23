namespace Airelax.Domain.Orders.Define
{
    public enum OrderState
    {
        Unfinished = 1,
        Established,
        Finish,
        Cancel,
        Processing,
        BeforeAppropriation
    }
}