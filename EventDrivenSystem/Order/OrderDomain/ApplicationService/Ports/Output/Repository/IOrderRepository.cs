using Rosered11.Common.Domain.ValueObject;
using Rosered11.Order.Domain.Core.ValueObject;

namespace Rosered11.Order.Application.Service.Ports.Output.Repository;

public interface IOrderRepository {

    Domain.Core.Entity.Order save(Domain.Core.Entity.Order order);

    Domain.Core.Entity.Order? findById(OrderId orderId);

    Domain.Core.Entity.Order? findByTrackingId(TrackingId trackingId);
}