using Rosered11.Common.Domain.ValueObject;
using Rosered11.Order.Application.Service.Ports.Output.Repository;
using Rosered11.Order.Domain.Core.ValueObject;

namespace Rosered11.Order.DataAccess.Order;

public class OrderRepository : IOrderRepository
{
    private readonly OrderDataAccessMapper _orderDataMapper;
    public OrderRepository(OrderDataAccessMapper orderDataMapper)
    {
        _orderDataMapper = orderDataMapper;
    }
    public virtual Domain.Core.Entity.Order save(Domain.Core.Entity.Order order) {
        // Mock
        return order;
    }

    public virtual Domain.Core.Entity.Order? findById(OrderId orderId) {
        // Mock
        return _orderDataMapper.orderEntityToOrder(new Entity.OrderEntity());
    }

    public virtual Domain.Core.Entity.Order? findByTrackingId(TrackingId trackingId) {
        // Mock
        return _orderDataMapper.orderEntityToOrder(new Entity.OrderEntity());
    }
}