using Common.CommonDomain.Entities;
using Common.CommonDomain.ValueObject;

namespace Rosered11.OrderService.Domain.Entities
{
    public class Restaurant : AggregateRoot<RestaurantId>
    {
        private readonly List<Product>? _products;
        private bool _active;

        public List<Product>? Products => _products;

        public bool IsActive { get => _active; }
        private Restaurant(Builder builder) : base(builder.RestaurantId)
        {
            _products = builder.Products;
            _active = builder.Active;
        }

        public static Builder NewBuilder()
        {
            return new Builder();
        }

        public sealed class Builder
        {
            public RestaurantId RestaurantId { get; private set; }
            public List<Product>? Products { get; private set; }
            public bool Active { get; private set; }

            public Builder SetRestaurantId(RestaurantId restaurantId)
            {
                this.RestaurantId = restaurantId;
                return this;
            }
            public Builder SetProducts(List<Product> products)
            {
                Products = products;
                return this;
            }
            public Builder SetActive(bool active)
            {
                Active = active;
                return this;
            }
            public Restaurant Build() => new(this);
        }
    }
}