namespace Rosered11.OrderService.Domain.DTO.Track
{
    public class TrackOrderQuery
    {
        private readonly Guid _orderTrackingId;

        public Guid OrderTrackingId => _orderTrackingId;
    }
}