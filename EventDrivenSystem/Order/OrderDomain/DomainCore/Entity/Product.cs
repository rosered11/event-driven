using Rosered11.Common.Domain.Entity;
using Rosered11.Common.Domain.ValueObject;

namespace Rosered11.Order.Domain.Core.Entity
{
    public class Product : BaseEntity<ProductId>
    {
        public string? Name { get; private set; }
        public Money? Price { get; private set; }

        public Product(ProductId id, string name, Money price) : base(id)
        {
            Name = name;
            Price = price;
        }
        public Product(ProductId id) : base(id)
        {
        }
        public void updateWithConfirmedNameAndPrice(string name, Money price) {
            Name = name;
            Price = price;
        }
    }
}