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
    }
}