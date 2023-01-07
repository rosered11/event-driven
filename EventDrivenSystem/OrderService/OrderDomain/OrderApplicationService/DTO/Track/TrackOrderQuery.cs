namespace Rosered11.OrderService.Domain.DTO.Track
{
    public class TrackOrderQuery
    {
        private readonly Guid _orderTrackingId;

        public Guid OrderTrackingId => _orderTrackingId;

        private TrackOrderQuery(Builder builder)
        {
            _orderTrackingId = builder.OrderTrackingId;
        }
        public static Builder NewBuilder()
        {
            return new Builder();
        }

        public sealed class Builder
        {
            public Guid OrderTrackingId { get; private set; }

            public Builder SetOrderTrackingId(Guid orderTrackingId)
            {
                OrderTrackingId = orderTrackingId;
                return this;
            }
            public TrackOrderQuery Build() => new(this);
        }
    }
}