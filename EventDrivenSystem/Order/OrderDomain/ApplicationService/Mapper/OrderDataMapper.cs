using System;
using System.Collections.Generic;
using System.Linq;
using Rosered11.Common.Domain.ValueObject;
using Rosered11.Order.Application.Service.DTO.Create;
using Rosered11.Order.Application.Service.DTO.Message;
using Rosered11.Order.Application.Service.DTO.Track;
using Rosered11.Order.Application.Service.Outbox.Model.Approval;
using Rosered11.Order.Application.Service.Outbox.Model.Payment;
using Rosered11.Order.Domain.Core.Entity;
using Rosered11.Order.Domain.Core.Event;
using Rosered11.Order.Domain.Core.ValueObject;

namespace Rosered11.Order.Application.Service.Mapper
{
    public class OrderDataMapper
    {
        public Restaurant createOrderCommandToRestaurant(CreateOrderCommand createOrderCommand) {
            return new Restaurant(new RestaurantId(createOrderCommand.RestaurantId)){
                Products = createOrderCommand.Items.Select(x => new Product(new(x.ProductId))).ToList()
            };
        }
        
        public Domain.Core.Entity.Order createOrderCommandToOrder(CreateOrderCommand createOrderCommand) {
            return new Order.Domain.Core.Entity.Order{
                CustomerId = new(createOrderCommand.CustomerId),
                RestaurantId = new(createOrderCommand.RestaurantId),
                DeliveryAddress = orderAddressToStreetAddress(createOrderCommand.Address),
                Price = new(createOrderCommand.Price),
                Items = orderItemsToOrderItemEntities(createOrderCommand.Items)
            };
        }

        public CreateOrderResponse orderToCreateOrderResponse(Domain.Core.Entity.Order order, String message) {
            return new CreateOrderResponse(
                order.TrackingId.GetValue(),
                order.OrderStatus,
                message
            );
        }

        public TrackOrderResponse orderToTrackOrderResponse(Domain.Core.Entity.Order order) {
            return new TrackOrderResponse(
                order.TrackingId.GetValue(),
                order.OrderStatus,
                order.FailureMessages
            );
        }

        public OrderPaymentEventPayload orderCreatedEventToOrderPaymentEventPayload(OrderCreatedEvent orderCreatedEvent) {
            return new OrderPaymentEventPayload(
                orderCreatedEvent.Order.ID.GetValue().ToString(),
                orderCreatedEvent.Order.CustomerId.GetValue().ToString(),
                orderCreatedEvent.Order.Price.Amount,
                orderCreatedEvent.CreatedAt,
                nameof(PaymentOrderStatus.PENDING)
                );
        }

        public OrderPaymentEventPayload orderCancelledEventToOrderPaymentEventPayload(OrderCancelledEvent
                                                                                            orderCancelledEvent) {
            return new OrderPaymentEventPayload(
                orderCancelledEvent.Order.ID.GetValue().ToString(),
                orderCancelledEvent.Order.CustomerId.GetValue().ToString(),
                orderCancelledEvent.Order.Price.Amount,
                orderCancelledEvent.CreatedAt,
                nameof(PaymentOrderStatus.CANCELLED)
            );
        }

        public OrderApprovalEventPayload orderPaidEventToOrderApprovalEventPayload(OrderPaidEvent orderPaidEvent) {
            return new OrderApprovalEventPayload(
                orderPaidEvent.Order.ID.GetValue().ToString(),
                orderPaidEvent.Order.RestaurantId.GetValue().ToString(),
                orderPaidEvent.Order.Price.Amount,
                orderPaidEvent.CreatedAt,
                nameof(RestaurantOrderStatus.PAID),
                orderPaidEvent.Order.Items.Select(x => new OrderApprovalEventProduct(
                    x.Product.ID.GetValue().ToString(),
                    x.Quantity
                ))
            );
        }

        public Customer customerModelToCustomer(CustomerModel customerModel) {
            return new Customer(new CustomerId(new Guid(customerModel.ID)),
                    customerModel.UserName,
                    customerModel.FirstName,
                    customerModel.LastName);
        }

        private List<Domain.Core.Entity.OrderItem> orderItemsToOrderItemEntities(
                IEnumerable<DTO.Create.OrderItem> orderItems) {
            return orderItems.Select(x => new Domain.Core.Entity.OrderItem(null){
                Product = new(new(x.ProductId)),
                Price = new(x.Price),
                Quantity = x.Quantity,
                SubTotal = new(x.SubTotal)
            }).ToList();
        }

        private StreetAddress orderAddressToStreetAddress(OrderAddress orderAddress) {
            return new StreetAddress(
                    Guid.NewGuid(),
                    orderAddress.Street,
                    orderAddress.PostalCode,
                    orderAddress.City
            );
        }
    }
}