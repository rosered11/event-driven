using Microsoft.Extensions.Logging;
using Rosered11.Common.Domain.ValueObject;
using Rosered11.Infrastructure.Saga;
using Rosered11.Order.Application.Service.Ports.Output.Repository;

namespace Rosered11.Order.Application.Service;

public class OrderSagaHelper
{
    private readonly ILogger _logger;
    private readonly IOrderRepository orderRepository;

    public OrderSagaHelper(ILogger<OrderSagaHelper> logger, IOrderRepository orderRepository) {
        _logger = logger;
        this.orderRepository = orderRepository;
    }

    Domain.Core.Entity.Order findOrder(String orderId) {
        Domain.Core.Entity.Order? orderResponse = orderRepository.findById(new OrderId(new(orderId)));
        if (orderResponse == null) {
            _logger.LogError("Order with id: {} could not be found!", orderId);
            throw new Exception("Order with id " + orderId + " could not be found!");
        }
        return orderResponse;
    }

    void saveOrder(Domain.Core.Entity.Order order) {
        orderRepository.save(order);
    }

    public SagaStatus orderStatusToSagaStatus(OrderStatus orderStatus) {
        switch (orderStatus) {
            case OrderStatus.PAID:
                return SagaStatus.PROCESSING;
            case OrderStatus.APPROVED:
                return SagaStatus.SUCCEEDED;
            case OrderStatus.CANCELLING:
                return SagaStatus.COMPENSATING;
            case OrderStatus.CANCELLED:
                return SagaStatus.COMPENSATED;
            default:
                return SagaStatus.STARTED;
        }
    }
}