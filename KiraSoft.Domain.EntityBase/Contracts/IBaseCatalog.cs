namespace KiraSoft.Domain.EntityBase.Contracts
{
    public interface IBaseCatalog<T> : IBaseEntity<T>, IBaseAuditable
    {
        string Name { get; }
        string ShortName { get; }
    }
}
