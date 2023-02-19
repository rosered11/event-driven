using Common.CommonDomain.ValueObject;
using Rosered11.OrderService.DataAccess.Entity;
using Rosered11.OrderService.Domain.Entities;
using Rosered11.OrderService.Domain.ValueObject;
using Rosered11.OrderService.Exception;

namespace Rosered11.OrderService.DataAccess.Mapper
{
    public class OrderDataAccessMapper
    {
        public OrderEntity OrderToOrderEntity(Order order)
        {
            if (order.DeliveryAddress == null || order.Id == null)
                throw new OrderDomainException("");
            OrderEntity orderEntity = new OrderEntity(order.Id.GetValue(), OrderItemToOrderItemEntities(order.Items), DeliveryAddressToAddressEntity(order.DeliveryAddress)){
                CustomerId = order.CustomerId?.GetValue() ?? default,
                RestaurantId = order.RestaurantId?.GetValue() ?? default,
                TrackingId = order.TrackingId?.GetValue() ?? default,
                Price = order.Price?.GetAmount() ?? default,
                Items = OrderItemToOrderItemEntities(order.Items),
                OrderStatus = order.OrderStatus,
                FailureMessage = order.FailureMessage != null ? string.Join(Order.FAILURE_MESSAGE_DELIMITER, order.FailureMessage) : string.Empty,
            };

            orderEntity.Address.OrderId = orderEntity.Id;
            orderEntity.Address.Order = orderEntity;
            orderEntity.Items.ForEach(x =>{ x.OrderId = orderEntity.Id; x.Order = orderEntity; });

            return orderEntity;

        }
        
        public Order OrderEntityToOrder(OrderEntity orderEntity)
        {
            return Order.NewBuilder(new OrderId(orderEntity.Id))
                .SetCustomerId(new CustomerId(orderEntity.CustomerId))
                .SetRestaurantId(new RestaurantId(orderEntity.RestaurantId))
                .SetDeliveryAddress(AddressEntityToDeliveryAddress(orderEntity.Address))
                .SetPrice(new Money(orderEntity.Price))
                .SetItems(OrderItemEntitiesToOrderItems(orderEntity.Items))
                .SetTrackingId(new TrackingId(orderEntity.TrackingId))
                .SetOrderStatus(orderEntity.OrderStatus)
                .SetFailureMessage(string.IsNullOrWhiteSpace(orderEntity.FailureMessage) ? new List<string>() : orderEntity.FailureMessage.Split(Order.FAILURE_MESSAGE_DELIMITER).ToList())
                .Build();
        }

        private List<OrderItem> OrderItemEntitiesToOrderItems(List<OrderItemEntity> items)
        {
            return items.Select(x => OrderItem
                    .NewBuilder(new OrderItemId(x.Id))
                    .SetProduct(new Product(new ProductId(x.ProductId)))
                    .SetPrice(new Money(x.Price))
                    .SetQuantity(x.Quantity)
                    .SetSubTotal(new Money(x.SubTotal))
                    .Build())
                .ToList();
        }

        private StreetAddress AddressEntityToDeliveryAddress(OrderAddressEntity address)
        {
            return new StreetAddress(address.Id, address.Street, address.PostalCode, address.City);
        }

        private List<OrderItemEntity> OrderItemToOrderItemEntities(List<OrderItem>? items)
        {
            if (items == null)
                return new ();
            return items.Select(x => new OrderItemEntity{
                Id = x.Id.GetValue(),
                ProductId = x.Product?.Id.GetValue() ?? default,
                Price = x.Price?.GetAmount() ?? default,
                Quantity = x.Quantity,
                SubTotal = x.SubTotal?.GetAmount() ?? default
            }).ToList();
        }

        public OrderAddressEntity DeliveryAddressToAddressEntity(StreetAddress deliveryAddress)
        {
            return new OrderAddressEntity{
                Id = deliveryAddress.Id,
                Street = deliveryAddress.Street,
                PostalCode = deliveryAddress.PostalCode,
                City = deliveryAddress.City,
            };
        }
    }
}