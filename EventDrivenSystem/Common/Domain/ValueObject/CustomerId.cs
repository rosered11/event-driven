namespace Rosered11.Common.Domain.ValueObject
{
    public class CustomerId : BaseId<Guid>
    {
        public CustomerId(Guid id) : base(id){}
    }
}