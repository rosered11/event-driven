namespace Common.CommonDomain.ValueObject
{
    public class OrderId : BaseId<Guid>
    {
        public OrderId(Guid value) : base(value)
        {
            
        }
    }
}