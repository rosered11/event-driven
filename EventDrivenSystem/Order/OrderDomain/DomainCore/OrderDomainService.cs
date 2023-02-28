using Microsoft.Extensions.Logging;
using Rosered11.Common.Domain.Entity;
using Rosered11.Order.Domain.Core.Entity;
using Rosered11.Order.Domain.Core.Event;
using Rosered11.Order.Domain.Core.Exception;

namespace Rosered11.Order.Domain.Core;

public class OrderDomainService
{
    private readonly ILogger<OrderDomainService> _logger;
    public OrderCreatedEvent validateAndInitiateOrder(Domain.Core.Entity.Order order, Restaurant restaurant) {
        validateRestaurant(restaurant);
        setOrderProductInformation(order, restaurant);
        order.validateOrder();
        order.initializeOrder();
        _logger.LogInformation("Order with id: {} is initiated", order.ID.GetValue());
        return new OrderCreatedEvent(order, ZoneDateTime.UtcNow());
    }

    public OrderPaidEvent payOrder(Domain.Core.Entity.Order order) {
        order.pay();
        _logger.LogInformation("Order with id: {} is paid", order.ID.GetValue());
        return new OrderPaidEvent(order, ZoneDateTime.UtcNow());
    }

    public void approveOrder(Domain.Core.Entity.Order order) {
        order.approve();
        _logger.LogInformation("Order with id: {} is approved", order.ID.GetValue());
    }

    public OrderCancelledEvent cancelOrderPayment(Domain.Core.Entity.Order order, List<string> failureMessages) {
        order.initCancel(failureMessages);
        _logger.LogInformation("Order payment is cancelling for order id: {}", order.ID.GetValue());
        return new OrderCancelledEvent(order, ZoneDateTime.UtcNow());
    }

    public void cancelOrder(Domain.Core.Entity.Order order, List<string> failureMessages) {
        order.cancel(failureMessages);
        _logger.LogInformation("Order with id: {} is cancelled", order.ID.GetValue());
    }

    private void validateRestaurant(Restaurant restaurant) {
        if (!restaurant.Active) {
            throw new OrderDomainException("Restaurant with id " + restaurant.ID.GetValue() +
                    " is currently not active!");
        }
    }

    private void setOrderProductInformation(Domain.Core.Entity.Order order, Restaurant restaurant) {
        order.Items.ToList().ForEach(orderItem => restaurant.Products.ToList().ForEach(restaurantProduct => {
            Product currentProduct = orderItem.Product;
            if (currentProduct.Equals(restaurantProduct))
            {
                currentProduct.updateWithConfirmedNameAndPrice(restaurantProduct.Name, restaurantProduct.Price);
            }
        }));
    }
}