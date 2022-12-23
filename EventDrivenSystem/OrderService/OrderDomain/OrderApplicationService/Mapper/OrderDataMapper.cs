using Common.CommonDomain.ValueObject;
using Rosered11.OrderService.Domain.DTO.Create;
using Rosered11.OrderService.Domain.Entities;
using Rosered11.OrderService.Domain.ValueObject;

namespace Rosered11.OrderService.Domain.Mapper
{
    public class OrderDataMapper
    {
        public Restaurant CreateOrderCommandToRestaurant(CreateOrderCommand createOrderCommand)
        {
            return Restaurant.NewBuilder()
                .SetRestaurantId(new RestaurantId(createOrderCommand.RestaurantId))
                .SetProducts(createOrderCommand.Items.Select(x => new Product(new (x.ProductId))).ToList())
                .Build();
        }

        public Order CreateOrderCommandToOrder(CreateOrderCommand createOrderCommand)
        {
            return Order.NewBuilder()
                .SetCustomerId(new CustomerId(createOrderCommand.CustomerId))
                .SetRestaurantId(new (createOrderCommand.RestaurantId))
                .SetDeliveryAddress(OrderAddressToStreetAddress(createOrderCommand.Address))
                .SetPrice(new Money(createOrderCommand.Price))
                .SetItems(OrderItemsToOrderItemEntities(createOrderCommand.Items))
                .Build();
        }

        public CreateOrderResponse OrderToCreateOrderResponse(Order order)
        {
            return CreateOrderResponse.NewBuilder()
                    .SetOrderTrackingId(order.TrackingId.GetValue())
                    .SetOrderStatus(order.OrderStatus)
                    .Build();
        }

        private IEnumerable<Entities.OrderItem> OrderItemsToOrderItemEntities(
            List<DTO.Create.OrderItem> orderItems)
        {
            return orderItems.Select(x => 
                Entities.OrderItem.NewBuilder()
                    .SetProduct(new Product(new ProductId(x.ProductId)))
                    .SetPrice(new Money(x.Price))
                    .SetQuantity(x.Quantity)
                    .SetSubTotal(new (x.SubTotal))
                    .Build());
        }

        private StreetAddress OrderAddressToStreetAddress(OrderAddress orderAddress)
        {
            return new StreetAddress(
                Guid.NewGuid()
                , orderAddress.Street
                , orderAddress.PostalCode
                , orderAddress.City);
        }
    }
}