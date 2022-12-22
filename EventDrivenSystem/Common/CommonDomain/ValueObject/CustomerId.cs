namespace Common.CommonDomain.ValueObject
{
    public class CustomerId : BaseId<Guid>
    {
        public CustomerId(Guid value) : base(value){}
    }
}