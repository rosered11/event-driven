using Rosered11.Common.Domain.ValueObject;
using Rosered11.Order.DataAccess.Order.Entity;
using Rosered11.Order.Domain.Core.Entity;
using Rosered11.Order.Domain.Core.ValueObject;

namespace Rosered11.Order.DataAccess.Order;

public class OrderDataAccessMapper
{
    public OrderEntity orderToOrderEntity(Domain.Core.Entity.Order order) {
        OrderEntity orderEntity = new OrderEntity{
            Id = order.ID.GetValue(),
            CustomerId = order.CustomerId.GetValue(),
            RestaurantId = order.RestaurantId.GetValue(),
            TrackingId = order.TrackingId.GetValue(),
            Address = deliveryAddressToAddressEntity(order.DeliveryAddress),
            Price = order.Price.Amount,
            Items = orderItemsToOrderItemEntities(order.Items),
            OrderStatus = order.OrderStatus.ToString(),
            FailureMessages = order.FailureMessages != null ? string.Join(Domain.Core.Entity.Order.FAILURE_MESSAGE_DELIMITER, order.FailureMessages) : string.Empty
        };
        // orderEntity.Address.Order = orderEntity;
        // orderEntity.Items.ToList().ForEach(orderItemEntity => orderItemEntity.Order = orderEntity);
        return orderEntity;
    }

    public Domain.Core.Entity.Order orderEntityToOrder(OrderEntity orderEntity) {
        return new Domain.Core.Entity.Order(orderEntity.Id){
            CustomerId = new(orderEntity.CustomerId),
            RestaurantId = new(orderEntity.RestaurantId),
            DeliveryAddress = addressEntityToDeliveryAddress(orderEntity.Address),
            Price = new(orderEntity.Price),
            Items = orderItemEntitiesToOrderItems(orderEntity.Items),
            TrackingId = new(orderEntity.TrackingId),
            OrderStatus = Enum.Parse<OrderStatus>(orderEntity.OrderStatus),
            FailureMessages = string.IsNullOrWhiteSpace(orderEntity.FailureMessages) 
                ? orderEntity.FailureMessages.Split(Domain.Core.Entity.Order.FAILURE_MESSAGE_DELIMITER).ToList() 
                : new ()
        };
    }

    private List<OrderItem> orderItemEntitiesToOrderItems(List<OrderItemEntity> items) {
        return items.Select(orderItemEntity => new OrderItem(new(orderItemEntity.Id)){
            Product = new(new(orderItemEntity.ProductId)),
            Price = new(orderItemEntity.Price),
            Quantity = orderItemEntity.Quantity,
            SubTotal = new(orderItemEntity.SubTotal)
        }).ToList();
    }

    private StreetAddress addressEntityToDeliveryAddress(OrderAddressEntity address) {
        return new StreetAddress(address.Id,
                address.Street,
                address.PostalCode,
                address.City);
    }

    private List<OrderItemEntity> orderItemsToOrderItemEntities(IEnumerable<OrderItem> items) {
        return items.Select(orderItem => new OrderItemEntity{
            Id = orderItem.ID.GetValue(),
            ProductId = orderItem.Product.ID.GetValue(),
            Price = orderItem.Price.Amount,
            Quantity = orderItem.Quantity,
            SubTotal = orderItem.SubTotal.Amount
        }).ToList();
    }

    private OrderAddressEntity deliveryAddressToAddressEntity(StreetAddress deliveryAddress) {
        return new OrderAddressEntity{
            Id = deliveryAddress.ID,
            Street = deliveryAddress.Street,
            PostalCode = deliveryAddress.PostalCode,
            City = deliveryAddress.City
        };
    }
}