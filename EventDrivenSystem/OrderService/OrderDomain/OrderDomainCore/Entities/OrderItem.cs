using Common.CommonDomain.Entities;
using Common.CommonDomain.ValueObject;
using Rosered11.OrderService.Domain.ValueObject;

namespace Rosered11.OrderService.Domain.Entities
{
    public class OrderItem : BaseEntity<OrderItemId>
    {
        private OrderId? _orderId;
        private readonly Product? _product;
        private readonly int _quantity;
        private readonly Money? _price;
        private readonly Money? _subTotal;

        public void InitializeOrderItem(OrderId? orderId, OrderItemId orderItemId)
        {
            _orderId = orderId;
            base.Id = orderItemId;
        }
        public bool IsPriceValid()
        {
            if (_price != null && _product != null)
                return _price.IsGreaterThanZero() && _price.Equals(_product.GetPrice()) && (_price * _quantity).Equals(_subTotal);
            return false;
        }
        private OrderItem(Builder builder) : base(builder.OrderItemId)
        {
            _product = builder.Product;
            _quantity = builder.Quantity;
            _price = builder.Price;
            _subTotal = builder.SubTotal;
        }

        public static Builder NewBuilder(OrderItemId id)
        {
            return new Builder(id);
        }

        public OrderId? OrderId => this._orderId;
        public Product? Product => this._product;
        public int Quantity => this._quantity;
        public Money? Price => this._price;
        public Money? SubTotal => this._subTotal;

        public sealed class Builder
        {
            public OrderItemId OrderItemId { get; private set;}
            public Builder(OrderItemId id) => OrderItemId = id;
            public Product? Product { get; private set;}
            public int Quantity { get; private set;}
            public Money? Price { get; private set;}
            public Money? SubTotal { get; private set;}

            public Builder SetOrderItemId(OrderItemId value)
            {
                OrderItemId = value;
                return this;
            }
            public Builder SetProduct(Product value)
            {
                Product = value;
                return this;
            }
            public Builder SetQuantity(int value)
            {
                Quantity = value;
                return this;
            }
            public Builder SetPrice(Money value)
            {
                Price = value;
                return this;
            }
            public Builder SetSubTotal(Money value)
            {
                SubTotal = value;
                return this;
            }
            public OrderItem Build() => new(this);
        }
    }
}