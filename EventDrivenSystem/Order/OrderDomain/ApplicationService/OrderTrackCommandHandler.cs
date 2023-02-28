using Microsoft.Extensions.Logging;
using Rosered11.Order.Application.Service.DTO.Track;
using Rosered11.Order.Application.Service.Mapper;
using Rosered11.Order.Application.Service.Ports.Output.Repository;
using Rosered11.Order.Domain.Core.ValueObject;

namespace Rosered11.Order.Application.Service;

public class OrderTrackCommandHandler
{
    private readonly ILogger<OrderTrackCommandHandler> _logger;
    private readonly OrderDataMapper orderDataMapper;

    private readonly IOrderRepository orderRepository;

    public OrderTrackCommandHandler(ILogger<OrderTrackCommandHandler> logger, OrderDataMapper orderDataMapper, IOrderRepository orderRepository) {
        _logger = logger;
        this.orderDataMapper = orderDataMapper;
        this.orderRepository = orderRepository;
    }

    // @Transactional(readOnly = true)
    public TrackOrderResponse trackOrder(Application.Service.DTO.Track.TrackOrderQuery trackOrderQuery) {
           Domain.Core.Entity.Order orderResult =
                   orderRepository.findByTrackingId(new TrackingId(trackOrderQuery.OrderTrackingId));
           if (orderResult == null) {
               _logger.LogWarning("Could not find order with tracking id: {}", trackOrderQuery.OrderTrackingId);
               throw new Exception("Could not find order with tracking id: " +
                       trackOrderQuery.OrderTrackingId);
           }
           return orderDataMapper.orderToTrackOrderResponse(orderResult);
    }
}