using Rosered11.Common.Domain.Entity;
using Rosered11.Common.Domain.ValueObject;
using Rosered11.Order.Domain.Core.Exception;
using Rosered11.Order.Domain.Core.ValueObject;

namespace Rosered11.Order.Domain.Core.Entity
{
    public class Order : AggregateRoot<OrderId>
    {
        public const string FAILURE_MESSAGE_DELIMITER = ",";

        public Order() : base(new OrderId(Guid.NewGuid()))
        {
        }
        public Order(Guid id) : base(new OrderId(id))
        {
        }

        public CustomerId? CustomerId { get; set; }
        public RestaurantId? RestaurantId { get; set; }
        public StreetAddress? DeliveryAddress { get; set; }
        public Money? Price { get; set; }
        public ICollection<OrderItem>? Items { get; set; }
        public TrackingId? TrackingId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public List<string> FailureMessages { get; set; } = new();

        public void initializeOrder() {
            TrackingId = new TrackingId(Guid.NewGuid());
            OrderStatus = OrderStatus.PENDING;
            initializeOrderItems();
        }

        public void validateOrder() {
            validateInitialOrder();
            validateTotalPrice();
            validateItemsPrice();
        }

        public void pay() {
            if (OrderStatus != OrderStatus.PENDING) {
                throw new OrderDomainException("Order is not in correct state for pay operation!");
            }
            OrderStatus = OrderStatus.PAID;
        }

        public void approve() {
            if(OrderStatus != OrderStatus.PAID) {
                throw new OrderDomainException("Order is not in correct state for approve operation!");
            }
            OrderStatus = OrderStatus.APPROVED;
        }

        public void initCancel(List<String> failureMessages) {
            if (OrderStatus != OrderStatus.PAID) {
                throw new OrderDomainException("Order is not in correct state for initCancel operation!");
            }
            OrderStatus = OrderStatus.CANCELLING;
            updateFailureMessages(failureMessages);
        }

        public void cancel(List<String> failureMessages) {
            if (!(OrderStatus == OrderStatus.CANCELLING || OrderStatus == OrderStatus.PENDING)) {
                throw new OrderDomainException("Order is not in correct state for cancel operation!");
            }
            OrderStatus = OrderStatus.CANCELLED;
            updateFailureMessages(failureMessages);
        }

        private void updateFailureMessages(List<string> failureMessages) {
            if (this.FailureMessages != null && failureMessages != null) {
                this.FailureMessages.AddRange(failureMessages.Where(x => string.IsNullOrEmpty(x)));
            }
            if (this.FailureMessages == null) {
                this.FailureMessages = failureMessages ?? new();
            }
        }

        private void validateInitialOrder() {
            if (ID != null) {
                throw new OrderDomainException("Order is not in correct state for initialization!");
            }
        }

        private void validateTotalPrice() {
            if (Price == null || !Price.isGreaterThanZero()) {
                throw new OrderDomainException("Total price must be greater than zero!");
            }
        }

        private void validateItemsPrice() {
            Money orderItemsTotal = Items?.Select(x => { validateItemPrice(x); return x.SubTotal; }).Aggregate(Money.Zero, (sum, subTotal) => sum.add(subTotal)) ?? Money.Zero;

            Price = Price ?? Money.Zero;
            if (!Price.Equals(orderItemsTotal)) {
                throw new OrderDomainException("Total price: " + Price.Amount
                    + " is not equal to Order items total: " + orderItemsTotal.Amount + "!");
            }
        }

        private void validateItemPrice(OrderItem orderItem) {
            if (!orderItem.isPriceValid()) {
                throw new OrderDomainException("Order item price: " + orderItem.Price.Amount +
                        " is not valid for product " + orderItem?.Product?.ID?.GetValue());
            }
        }

        private void initializeOrderItems() {
            long itemId = 1;
            foreach (OrderItem orderItem in Items) {
                orderItem.initializeOrderItem(base.ID, new OrderItemId(itemId++));
            }
        }
    }
}