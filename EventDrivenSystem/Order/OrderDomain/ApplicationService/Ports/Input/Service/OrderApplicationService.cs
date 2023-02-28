using Rosered11.Order.Application.Service.DTO.Create;
using Rosered11.Order.Application.Service.DTO.Track;

namespace Rosered11.Order.Application.Service.Ports.Input.Service;

public class OrderApplicationService
{
    private readonly OrderCreateCommandHandler orderCreateCommandHandler;

    private readonly OrderTrackCommandHandler orderTrackCommandHandler;

    public OrderApplicationService(OrderCreateCommandHandler orderCreateCommandHandler,
                                       OrderTrackCommandHandler orderTrackCommandHandler) {
        this.orderCreateCommandHandler = orderCreateCommandHandler;
        this.orderTrackCommandHandler = orderTrackCommandHandler;
    }

    // @Override
    public CreateOrderResponse createOrder(CreateOrderCommand createOrderCommand) {
        return orderCreateCommandHandler.createOrder(createOrderCommand);
    }

    // @Override
    public TrackOrderResponse trackOrder(TrackOrderQuery trackOrderQuery) {
        return orderTrackCommandHandler.trackOrder(trackOrderQuery);
    }
}