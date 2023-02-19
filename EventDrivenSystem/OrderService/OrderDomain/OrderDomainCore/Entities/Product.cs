using Common.CommonDomain.Entities;
using Common.CommonDomain.ValueObject;

namespace Rosered11.OrderService.Domain.Entities
{
    public class Product : BaseEntity<ProductId>
    {
        private string? name;
        private Money? price;

        public Product(ProductId productId) : base(productId)
        {
        }
        public Product(ProductId productId, string name, Money price) : base(productId)
        {
            this.name = name;
            this.price = price;
        }

        public void UpdateWithConfirmedNameAndPrice(string? name, Money? price)
        {
            this.name = name;
            this.price = price;
        }

        public string? GetName() => this.name;
        public Money? GetPrice() => this.price;
    }
}