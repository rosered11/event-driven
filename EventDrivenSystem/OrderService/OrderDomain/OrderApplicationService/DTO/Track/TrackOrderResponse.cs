using Common.CommonDomain.ValueObject;

namespace Rosered11.OrderService.Domain.DTO.Track
{
    public class TrackOrderResponse
    {
        private readonly Guid _orderTrackingId;
        private readonly OrderStatus _orderStatus;
        private readonly List<string> failureMessage;
    }
}