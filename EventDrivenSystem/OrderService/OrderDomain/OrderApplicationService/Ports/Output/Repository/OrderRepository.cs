using Rosered11.OrderService.Domain.Entities;
using Rosered11.OrderService.Domain.ValueObject;

namespace Rosered11.OrderService.Domain.Ports.Output.Repository
{
    public interface OrderRepository
    {
        Order Save(Order order);
        Order FindByTrackingId(TrackingId trackingId);
    }
}