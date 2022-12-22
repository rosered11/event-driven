using Microsoft.Extensions.Logging;
using Rosered11.OrderService.Domain.Entities;
using Rosered11.OrderService.Domain.Event;
using Rosered11.OrderService.Order.Exception;

namespace Rosered11.OrderService.Domain
{
    public class OrderDomainService : IOrderDomainService
    {
        private static DateTimeOffset DateTimeNow() => new DateTimeOffset(DateTime.Now, TimeSpan.FromHours(7));
        private readonly ILogger<OrderDomainService> _logger;

        public OrderDomainService(ILogger<OrderDomainService> logger)
        {
            _logger = logger;
        }
        public void ApproveOrder(Entities.Order? order)
        {
            order?.Approve();
            _logger.LogInformation("Order with id: {Id} is approved", order?.Id?.GetValue());
        }

        public void CancelOrder(Entities.Order? order, List<string> failureMessage)
        {
            order?.Cancel(failureMessage);
            _logger.LogInformation("Order with id: {Id} is cancelled", order?.Id?.GetValue());
        }

        public OrderCancelledEvent CancelOrderPayment(Entities.Order? order, List<string> failureMessage)
        {
            order?.InitCancel(failureMessage);
            _logger.LogInformation("Order payment is cancelling for order id: {Id}", order?.Id?.GetValue());
            return new(order, DateTimeNow());
        }

        public OrderPaidEvent PayOrder(Entities.Order? order)
        {
            order?.Pay();
            _logger.LogInformation("Order with id: {Id} is paid", order?.Id?.GetValue());
            return new(order, DateTimeNow());
        }

        public OrderCreatedEvent ValidateAndInitiateOrder(Entities.Order? order, Restaurant? restaurant)
        {
            ValidateRestaurant(restaurant);
            SetOrderProductInformation(order, restaurant);
            order?.ValidateOrder();
            order?.InitializeOrder();
            _logger.LogInformation("Order with id: {Id} is initiated", order?.Id?.GetValue());
            return new(order, DateTimeNow());
        }

        private void SetOrderProductInformation(Entities.Order? order, Restaurant? restaurant)
        {
            order?.Items?.ToList().ForEach(orderItem => restaurant?.Products?.ToList().ForEach(restaurantProduct => {
                Product? currentProduct = orderItem?.Product;
                if (currentProduct != null && currentProduct.Equals(restaurantProduct))
                {
                    currentProduct.UpdateWithConfirmedNameAndPrice(
                        restaurantProduct.GetName()
                        , restaurantProduct.GetPrice());
                }
            }));
        }

        private void ValidateRestaurant(Restaurant? restaurant)
        {
            if (restaurant != null && !restaurant.IsActive)
                throw new OrderDomainException($"Restaurant with id {restaurant?.Id?.GetValue()} is currently not active!");
        }
    }
}