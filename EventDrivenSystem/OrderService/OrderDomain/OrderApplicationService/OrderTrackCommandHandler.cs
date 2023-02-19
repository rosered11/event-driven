using Microsoft.Extensions.Logging;
using Rosered11.OrderService.Domain.DTO.Track;
using Rosered11.OrderService.Domain.Entities;
using Rosered11.OrderService.Domain.Mapper;
using Rosered11.OrderService.Domain.Ports.Output.Repository;
using Rosered11.OrderService.Exception;

namespace Rosered11.OrderService.Domain
{
    public class OrderTrackCommandHandler
    {
        private readonly ILogger<OrderTrackCommandHandler> _logger;
        private readonly OrderDataMapper _orderDataMapper;
        private readonly IOrderRepository _orderRepository;

        public OrderTrackCommandHandler(ILogger<OrderTrackCommandHandler> logger, OrderDataMapper orderDataMapper, IOrderRepository orderRepository)
        {
            _logger = logger;
            _orderDataMapper = orderDataMapper;
            _orderRepository = orderRepository;
        }

        public TrackOrderResponse TrackOrder(TrackOrderQuery trackOrderQuery)
        {
            Order orderResult = _orderRepository.FindByTrackingId(new ValueObject.TrackingId(trackOrderQuery.OrderTrackingId));
            if (orderResult == null)
            {
                _logger.LogWarning("Could not find order with tracking id: {Id}", trackOrderQuery.OrderTrackingId);
                throw new OrderNotFoundException($"Could not find order with tracking id: {trackOrderQuery.OrderTrackingId}");
            }
            return _orderDataMapper.OrderToTrackOrderResponse(orderResult);
        }
    }
}