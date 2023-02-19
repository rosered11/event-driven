namespace Common.CommonDomain.Entities
{
    public abstract class AggregateRoot<ID> : BaseEntity<ID>
    {
        protected AggregateRoot(ID id) : base(id)
        {
        }
    }
}