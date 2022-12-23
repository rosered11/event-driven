using Rosered11.OrderService.Domain.DTO.Create;
using Rosered11.OrderService.Domain.DTO.Track;

namespace Rosered11.OrderService.Domain.Ports.Input.Service
{
    public interface IOrderApplicationService
    {
        CreateOrderResponse CreateOrder(CreateOrderCommand createOrderCommand);
        TrackOrderResponse TrackOrder(TrackOrderQuery trackOrderQuery);
    }
}