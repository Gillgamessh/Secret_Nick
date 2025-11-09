namespace Epam.ItMarathon.ApiService.Domain.Abstract
{
    /// <summary>
    /// Base builder for aggregates.
    /// </summary>
    /// <typeparam name="TFactory"><inheritdoc/></typeparam>
    public abstract class BaseAggregateBuilder<TFactory> : BaseEntityBuilder<TFactory>
        where TFactory : BaseEntityBuilder<TFactory>;
}