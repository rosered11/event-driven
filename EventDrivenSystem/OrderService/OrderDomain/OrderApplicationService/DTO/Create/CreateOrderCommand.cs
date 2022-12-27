namespace Rosered11.OrderService.Domain.DTO.Create
{
    public class CreateOrderCommand
    {
        private readonly Guid _customerId;
        private readonly Guid _restaurantId;
        private readonly decimal _price;
        private readonly List<OrderItem> _items;
        private readonly OrderAddress _address;

        public Guid CustomerId => _customerId;

        public Guid RestaurantId => _restaurantId;

        public decimal Price => _price;

        public List<OrderItem> Items => _items;

        public OrderAddress Address => _address;

        private CreateOrderCommand(Builder builder)
        {
            _customerId = builder.CustomerId;
            _restaurantId = builder.RestaurantId;
            _price = builder.Price;
            _items = builder.Items;
            _address = builder.Address;
        }
        public static Builder NewBuilder()
        {
            return new Builder();
        }

        public sealed class Builder
        {
            public Guid CustomerId { get; private set; }
            public Guid RestaurantId { get; private set; }
            public decimal Price { get; private set; }
            public List<OrderItem> Items { get; private set; }
            public OrderAddress Address { get; private set; }

            public Builder SetCustomerId(Guid customerId)
            {
                CustomerId = customerId;
                return this;
            }
            public Builder SetRestaurantId(Guid restaurantId)
            {
                RestaurantId = restaurantId;
                return this;
            }
            public Builder SetPrice(decimal price)
            {
                Price = price;
                return this;
            }
            public Builder SetItems(List<OrderItem> items)
            {
                Items = items;
                return this;
            }
            public Builder SetAddress(OrderAddress address)
            {
                Address = address;
                return this;
            }
            public CreateOrderCommand Build() => new(this);
        }
    }
}