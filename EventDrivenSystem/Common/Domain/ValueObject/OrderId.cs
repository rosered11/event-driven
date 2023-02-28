namespace Rosered11.Common.Domain.ValueObject
{
    public class OrderId : BaseId<Guid>
    {
        public OrderId(Guid id) : base(id){}
    }
}