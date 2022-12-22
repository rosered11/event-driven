namespace Common.CommonDomain.ValueObject
{
    public class ProductId : BaseId<Guid>
    {
        public ProductId(Guid value) : base(value)
        {
        }
    }
}