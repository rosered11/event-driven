using Microsoft.Extensions.Logging;
using Rosered11.Infrastructure.Outbox;
using Rosered11.Order.Application.Service.DTO.Create;
using Rosered11.Order.Application.Service.Mapper;
using Rosered11.Order.Application.Service.Outbox.Scheduler;
using Rosered11.Order.Domain.Core.Event;

namespace Rosered11.Order.Application.Service.Ports;

public class OrderCreateCommandHandler
{
    private readonly ILogger<OrderCreateCommandHandler> _logger;
    private readonly OrderCreateHelper orderCreateHelper;
    private readonly OrderDataMapper orderDataMapper;
    private readonly PaymentOutboxHelper paymentOutboxHelper;
    private readonly OrderSagaHelper orderSagaHelper;

    public OrderCreateCommandHandler(ILogger<OrderCreateCommandHandler> logger,
            OrderCreateHelper orderCreateHelper,
                                     OrderDataMapper orderDataMapper,
                                     PaymentOutboxHelper paymentOutboxHelper,
                                     OrderSagaHelper orderSagaHelper) {
        _logger = logger;
        this.orderCreateHelper = orderCreateHelper;
        this.orderDataMapper = orderDataMapper;
        this.paymentOutboxHelper = paymentOutboxHelper;
        this.orderSagaHelper = orderSagaHelper;
    }

    // @Transactional
    public CreateOrderResponse createOrder(CreateOrderCommand createOrderCommand) {
        OrderCreatedEvent orderCreatedEvent = orderCreateHelper.persistOrder(createOrderCommand);
        _logger.LogInformation("Order is created with id: {}", orderCreatedEvent.Order.ID.GetValue());
        CreateOrderResponse createOrderResponse = orderDataMapper.orderToCreateOrderResponse(orderCreatedEvent.Order,
                "Order created successfully");

        paymentOutboxHelper.savePaymentOutboxMessage(orderDataMapper
                .orderCreatedEventToOrderPaymentEventPayload(orderCreatedEvent),
                orderCreatedEvent.Order.OrderStatus,
                orderSagaHelper.orderStatusToSagaStatus(orderCreatedEvent.Order.OrderStatus),
                OutboxStatus.STARTED,
                Guid.NewGuid());

        _logger.LogInformation("Returning CreateOrderResponse with order id: {}", orderCreatedEvent.Order.ID);

        return createOrderResponse;
    }
}