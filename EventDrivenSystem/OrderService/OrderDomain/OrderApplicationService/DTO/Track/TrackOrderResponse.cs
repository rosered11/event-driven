using Common.CommonDomain.ValueObject;

namespace Rosered11.OrderService.Domain.DTO.Track
{
    public class TrackOrderResponse
    {
        private readonly Guid _orderTrackingId;
        private readonly OrderStatus _orderStatus;
        private readonly List<string> failureMessage;

        private TrackOrderResponse(Builder builder)
        {
            _orderTrackingId = builder.OrderTrackingId;
            _orderStatus = builder.OrderStatus;
            failureMessage = builder.FailureMessage;
        }
        public static Builder NewBuilder()
        {
            return new Builder();
        }

        public sealed class Builder
        {
            public Guid OrderTrackingId { get; private set; }
            public OrderStatus OrderStatus { get; private set; }
            public List<string> FailureMessage { get; private set; }

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
            public Builder SetFailureMessage(List<string> failureMessage)
            {
                FailureMessage = failureMessage;
                return this;
            }
            public TrackOrderResponse Build() => new(this);
        }
    }
}