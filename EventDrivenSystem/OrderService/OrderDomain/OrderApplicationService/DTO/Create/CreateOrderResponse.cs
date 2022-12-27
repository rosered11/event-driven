using Common.CommonDomain.ValueObject;

namespace Rosered11.OrderService.Domain.DTO.Create
{
    public class CreateOrderResponse
    {
        private readonly Guid _orderTrackingId;
        private readonly OrderStatus _orderStatus;
        private readonly string _message;

        private CreateOrderResponse(Builder builder)
        {
            _orderTrackingId = builder.OrderTrackingId;
            _orderStatus = builder.OrderStatus;
            _message = builder.Message;
        }

        public Guid OrderTrackingId => _orderTrackingId;

        public OrderStatus OrderStatus => _orderStatus;

        public string Message => _message;

        public static Builder NewBuilder()
        {
            return new Builder();
        }
        public sealed class Builder
        {
            public Guid OrderTrackingId { get; private set; }
            public OrderStatus OrderStatus { get; private set; }
            public string Message { get; private set; }

            public Builder SetOrderTrackingId(Guid orderTrackingId)
            {
                OrderTrackingId = orderTrackingId;
                return this;
            }
            public Builder SetOrderStatus(OrderStatus orderStatus)
            {
                OrderStatus = orderStatus;
                return this;
            }
            public Builder SetMessage(string message)
            {
                Message = message;
                return this;
            }
            public CreateOrderResponse Build() => new(this);
        }
    }
}