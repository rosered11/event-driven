using Microsoft.Extensions.Logging;
using Rosered11.OrderService.Domain.DTO.Create;
using Rosered11.OrderService.Domain.Entities;
using Rosered11.OrderService.Domain.Event;
using Rosered11.OrderService.Domain.Mapper;
using Rosered11.OrderService.Domain.Ports.Output.Message.Publisher.Payment;
using Rosered11.OrderService.Domain.Ports.Output.Repository;
using Rosered11.OrderService.Exception;

namespace Rosered11.OrderService.Domain
{
    public class OrderCreateCommandHandler
    {
        private readonly ILogger<OrderCreateCommandHandler> _logger;
        private readonly OrderCreateHelper _orderCreateHelper;
        private readonly OrderDataMapper _orderDataMapper;
        private readonly OrderCreatedPaymentRequestMessagePublisher _orderCreatedPaymentRequestMessagePublisher;

        public OrderCreateCommandHandler(ILogger<OrderCreateCommandHandler> logger, OrderCreateHelper orderCreateHelper, OrderDataMapper orderDataMapper, OrderCreatedPaymentRequestMessagePublisher orderCreatedPaymentRequestMessagePublisher)
        {
            _logger = logger;
            _orderCreateHelper = orderCreateHelper;
            _orderDataMapper = orderDataMapper;
            _orderCreatedPaymentRequestMessagePublisher = orderCreatedPaymentRequestMessagePublisher;
        }

        public CreateOrderResponse CreateOrder(CreateOrderCommand createOrderCommand)
        {
            OrderCreatedEvent orderCreatedEvent = _orderCreateHelper.PersistOrder(createOrderCommand);
            _logger.LogInformation("Order is created with id: {Id}", orderCreatedEvent.Order.Id.GetValue());
            _orderCreatedPaymentRequestMessagePublisher.Publish(orderCreatedEvent);
            return _orderDataMapper.OrderToCreateOrderResponse(orderCreatedEvent.Order);
        }
    }
}