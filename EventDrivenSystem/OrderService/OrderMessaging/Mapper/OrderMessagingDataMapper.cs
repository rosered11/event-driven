using Rosered11.Kafka.Model;
using Rosered11.OrderService.Common.ValueObject;
using Rosered11.OrderService.Domain.DTO.Message;
using Rosered11.OrderService.Domain.Entities;
using Rosered11.OrderService.Domain.Event;

namespace Rosered11.OrderService.Messaging.Mapper
{
    public class OrderMessagingDataMapper
    {
        public PaymentRequestModel OrderCreatedEventToPaymentRequestModel(OrderCreatedEvent orderCreatedEvent)
        {
            if (orderCreatedEvent.Order == null)
                throw new System.Exception("");
            Order order = orderCreatedEvent.Order;
            return new PaymentRequestModel{
                Id = Guid.NewGuid().ToString(),
                SagaId = string.Empty,
                CustomerId = order.CustomerId?.GetValue().ToString(),
                OrderId = order.Id.GetValue().ToString(),
                Price = order.Price?.GetAmount() ?? default,
                CreatedAt = orderCreatedEvent.CreatedAt,
                PaymentOrderStatus = PaymentOrderStatus.PENDING,
                
            };
        }

        public PaymentRequestModel OrderCancelledEventToPaymentRequestModel(OrderCancelledEvent orderCancelledEvent)
        {
            if (orderCancelledEvent.Order == null)
                throw new System.Exception("");
            Order order = orderCancelledEvent.Order;
            return new PaymentRequestModel{
                Id = Guid.NewGuid().ToString(),
                SagaId = string.Empty,
                CustomerId = order.CustomerId?.GetValue().ToString(),
                OrderId = order.Id.GetValue().ToString(),
                Price = order.Price?.GetAmount() ?? default,
                CreatedAt = orderCancelledEvent.CreatedAt,
                PaymentOrderStatus = PaymentOrderStatus.CANCELLED,
                
            };
        }

        public RestaurantApprovalRequestModel OrderPaidEventToRestaurantApprovalRequestModel(OrderPaidEvent orderPaidEvent)
        {
            if (orderPaidEvent.Order == null)
                throw new System.Exception("");
            Order order = orderPaidEvent.Order;
            return new RestaurantApprovalRequestModel{
                Id = Guid.NewGuid().ToString(),
                SagaId = string.Empty,
                OrderId = order.Id.GetValue().ToString(),
                RestaurantId = order.RestaurantId?.GetValue().ToString(),
                RestaurantOrderStatus = RestaurantOrderStatus.PAID,
                Products = order.Items.Select(x => new RestaurantApprovalRequestModel.Product(x.Product?.Id.GetValue().ToString(), x.Quantity)).ToList(),
                Price = order.Price?.GetAmount() ?? default,
                CreatedAt = orderPaidEvent.CreatedAt
            };
        }

        public PaymentResponse PaymentResponseModelToPaymentResponse(PaymentResponseModel paymentResponseModel)
        {
            return new PaymentResponse {
                Id = paymentResponseModel.Id,
                SagaId = paymentResponseModel.SagaId,
                PaymentId = paymentResponseModel.PaymentId,
                CustomerId = paymentResponseModel.CustomerId,
                OrderId = paymentResponseModel.OrderId,
                Price = paymentResponseModel.Price,
                CreatedAt = paymentResponseModel.CreatedAt,
                PaymentStatus = paymentResponseModel.PaymentStatus,
                FailureMessage = paymentResponseModel.FailureMessages,  
            };
        }

        public RestaurantApprovalResponse ApprovalResponseModelToApprovalResponse(RestaurantApprovalResponseModel restaurantApprovalResponseModel)
        {
            return new RestaurantApprovalResponse{
                Id = restaurantApprovalResponseModel.Id,
                SagaId = restaurantApprovalResponseModel.SagaId,
                RestaurantId = restaurantApprovalResponseModel.RestaurantId,
                OrderId = restaurantApprovalResponseModel.OrderId,
                CreatedAt = restaurantApprovalResponseModel.CreatedAt,
                OrderApprovalStatus = restaurantApprovalResponseModel.OrderApprovalStatus,
                FailureMessage = restaurantApprovalResponseModel.FailureMessages
            };
        }
    }
}