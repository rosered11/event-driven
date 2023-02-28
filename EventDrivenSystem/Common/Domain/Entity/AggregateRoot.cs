namespace Rosered11.Common.Domain.Entity
{
    public class AggregateRoot<ID> : BaseEntity<ID>
    {
        public AggregateRoot(ID id) : base(id)
        {
        }
    }
}