namespace Rosered11.Common.Domain.ValueObject
{
    public class ProductId : BaseId<Guid>
    {
        public ProductId(Guid value) : base(value)
        {
        }
    }
}