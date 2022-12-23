using Rosered11.OrderService.Domain.DTO.Create;
using Rosered11.OrderService.Domain.DTO.Track;
using Rosered11.OrderService.Domain.Ports.Input.Service;

namespace Rosered11.OrderService.Domain.Ports
{
    public class OrderApplicationService : IOrderApplicationService
    {
        private readonly OrderCreateCommandHandler _orderCreateCommandHandler;
        private readonly OrderTrackCommandHandler _orderTrackCommandHandler;
        public OrderApplicationService(OrderCreateCommandHandler orderCreateCommandHandler, OrderTrackCommandHandler orderTrackCommandHandler)
        {
            _orderCreateCommandHandler = orderCreateCommandHandler;
            _orderTrackCommandHandler = orderTrackCommandHandler;
        }
        public CreateOrderResponse CreateOrder(CreateOrderCommand createOrderCommand)
        {
            throw new NotImplementedException();
        }

        public TrackOrderResponse TrackOrder(TrackOrderQuery trackOrderQuery)
        {
            throw new NotImplementedException();
        }
    }
}