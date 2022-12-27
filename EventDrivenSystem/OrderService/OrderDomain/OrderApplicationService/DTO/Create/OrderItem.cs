namespace Rosered11.OrderService.Domain.DTO.Create
{
    public class OrderItem
    {
        private readonly Guid _productId;
        private readonly int _quantity;
        private readonly decimal _price;
        private readonly decimal _subTotal;

        public Guid ProductId => _productId;

        public int Quantity => _quantity;

        public decimal Price => _price;

        public decimal SubTotal => _subTotal;

        private OrderItem(Builder builder)
        {
            _productId = builder.ProductId;
            _quantity = builder.Quantity;
            _price = builder.Price;
            _subTotal = builder.SubTotal;
        }
        public static Builder NewBuilder()
        {
            return new Builder();
        }
        public sealed class Builder
        {
            public Guid ProductId { get; private set; }
            public int Quantity { get; private set; }
            public decimal Price { get; private set; }
            public decimal SubTotal { get; private set; }

            public Builder SetProductId(Guid productId)
            {
                ProductId = productId;
                return this;
            }
            public Builder SetQuantity(int quantity)
            {
                Quantity = quantity;
                return this;
            }
            public Builder SetPrice(decimal price)
            {
                Price = price;
                return this;
            }
            public Builder SetSubTotal(decimal subTotal)
            {
                SubTotal = subTotal;
                return this;
            }
            public OrderItem Build() => new(this);
        }
    }
}