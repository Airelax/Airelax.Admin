namespace Airelax.Domain.Houses.Defines
{
    public enum CancelPolicy
    {
        Elasticity = 1,
        ElasticityOrNotRefund,
        Medium,
        MediumOrNotRefund,
        LittleStrict,
        LittleStrictOrNotRefund,
        Strict,
        StrictOrNotRefund
    }
}