using Common.CommonDomain.Entities;
using Common.CommonDomain.ValueObject;
using Rosered11.OrderService.Domain.ValueObject;
using Rosered11.OrderService.Exception;

namespace Rosered11.OrderService.Domain.Entities
{
    public class Order : AggregateRoot<OrderId>
    {
        private readonly CustomerId? _customerId;
        private readonly RestaurantId? _restaurantId;
        private readonly StreetAddress? _deliveryAddress;
        private readonly Money? _price;
        private readonly List<OrderItem>? _items;
        private TrackingId? _trackingId;
        private OrderStatus _orderStatus;
        private List<string>? _failureMessage;

        public void InitializeOrder()
        {
            Id = new (Guid.NewGuid());
            _trackingId = new (Guid.NewGuid());
            _orderStatus = OrderStatus.PENDING;
        }

        private void InitializeOrderItem()
        {
            long itemId = 1;
            if (_items != null)
                foreach (var orderItem in _items)
                {
                    orderItem.InitializeOrderItem(Id, new OrderItemId(itemId++));
                }
        }
        public void ValidateOrder()
        {
            ValidateInitialOrder();
            ValidateTotalPrice();
            ValidateItemPrice();
        }
        public void Pay()
        {
            if (_orderStatus != OrderStatus.PENDING)
                throw new OrderDomainException("Order is not in correct state for pay operation!");
            _orderStatus = OrderStatus.PAID;
        }
        public void Approve()
        {
            if (_orderStatus != OrderStatus.PAID)
                throw new OrderDomainException("Order is not in correct state for approve operation!");
            _orderStatus = OrderStatus.APPROVED;
        }
        public void InitCancel(ICollection<string> failureMessage)
        {
            if (_orderStatus != OrderStatus.PAID)
                throw new OrderDomainException("Order is not in correct state for initCancel operation!");
            _orderStatus = OrderStatus.CANCELLING;
            UpdateFailureMessage(failureMessage);
        }

        private void UpdateFailureMessage(ICollection<string> failureMessage)
        {
            if (this._failureMessage != null && failureMessage != null)
            {
                this._failureMessage.AddRange(failureMessage.Where(x => !string.IsNullOrWhiteSpace(x)));
            }

            if (this._failureMessage == null)
                this._failureMessage = failureMessage?.ToList();
        }

        public void Cancel(ICollection<string> failureMessage)
        {
            if (!(_orderStatus == OrderStatus.CANCELLING || _orderStatus == OrderStatus.PENDING))
                throw new OrderDomainException("Order is not in correct state for cancel operation!");
            _orderStatus = OrderStatus.CANCELLED;
            UpdateFailureMessage(failureMessage);
        }
        private void ValidateItemPrice()
        {
            Money orderItemTotal = Money.Zero;
            if (_items != null)
            _items.ToList().ForEach(orderItem => {
                ValidateItemPrice(orderItem);
                if (orderItem.SubTotal != null)
                    orderItemTotal += orderItem.SubTotal;
            });

            if (_price != null && !_price.Equals(orderItemTotal))
                throw new OrderDomainException($"Total price: {_price.GetAmount()} isn't equal Order items total: {orderItemTotal.GetAmount()}!");
        }

        private void ValidateItemPrice(OrderItem orderItem)
        {
            if (!orderItem.IsPriceValid())
                throw new OrderDomainException($"Order item price: {orderItem.Price?.GetAmount()} is not valid for product {orderItem.Product?.Id?.GetValue()}");
        }

        private void ValidateInitialOrder()
        {
            if (Id != null)
            {
                throw new OrderDomainException("Order isn't in correct state for initialization!");
            }
        }

        private void ValidateTotalPrice()
        {
            if (_price == null || !_price.IsGreaterThanZero())
            {
                throw new OrderDomainException("Total price must be greater than zero!");
            }
        }

        private Order(Builder builder)
        {
            base.Id = builder.OrderId;
            _customerId = builder.CustomerId;
            _restaurantId = builder.RestaurantId;
            _deliveryAddress = builder.DeliveryAddress;
            _price = builder.Price;
            _items = builder.Items;
            _trackingId = builder.TrackingId;
            _orderStatus = builder.OrderStatus;
            _failureMessage = builder.FailureMessage;
        }

        public static Builder NewBuilder()
        {
            return new Builder();
        }

        public CustomerId? CustomerId => _customerId;

        public RestaurantId? RestaurantId => _restaurantId;

        public StreetAddress? DeliveryAddress => _deliveryAddress;

        public Money? Price => _price;

        public List<OrderItem>? Items => _items;

        public TrackingId? TrackingId { get => _trackingId; }
        public OrderStatus OrderStatus { get => _orderStatus; }
        public List<string>? FailureMessage { get => _failureMessage; }

        public sealed class Builder
        {
            public OrderId? OrderId { get; private set; }
            public CustomerId? CustomerId { get; private set; }
            public RestaurantId? RestaurantId { get; private set; }
            public StreetAddress? DeliveryAddress { get; private set; }
            public Money? Price { get; private set; }
            public List<OrderItem>? Items { get; private set; }
            public TrackingId? TrackingId { get; private set; }
            public OrderStatus OrderStatus { get; private set; }
            public List<string>? FailureMessage { get; private set; }

            public Builder SetOrderId(OrderId orderId)
            {
                OrderId = orderId;
                return this;
            }
            public Builder SetCustomerId(CustomerId customerId)
            {
                CustomerId = customerId;
                return this;
            }
            public Builder SetRestaurantId(RestaurantId restaurantId)
            {
                RestaurantId = restaurantId;
                return this;
            }
            public Builder SetDeliveryAddress(StreetAddress deliveryAddress)
            {
                DeliveryAddress = deliveryAddress;
                return this;
            }
            public Builder SetPrice(Money price)
            {
                Price = price;
                return this;
            }
            public Builder SetItems(List<OrderItem> items)
            {
                Items = items;
                return this;
            }
            public Builder SetTrackingId(TrackingId trackingId)
            {
                TrackingId = trackingId;
                return this;
            }
            public Builder SetOrderStatus(OrderStatus orderStatus)
            {
                OrderStatus = orderStatus;
                return this;
            }
            public Builder SetFailureMessage(List<string> failureMessage)
            {
                FailureMessage = failureMessage;
                return this;
            }
            public Order Build() => new(this);
        }
    }
}