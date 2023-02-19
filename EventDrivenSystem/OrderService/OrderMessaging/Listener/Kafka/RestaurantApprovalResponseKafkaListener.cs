using System.Text.Json;
using Microsoft.Extensions.Logging;
using Rosered11.Kafka.KafkaConsumer;
using Rosered11.Kafka.Model;
using Rosered11.OrderService.Common.ValueObject;
using Rosered11.OrderService.Domain.Entities;
using Rosered11.OrderService.Domain.Ports.Input.Message.Listener.RestaurantApproval;
using Rosered11.OrderService.Messaging.Mapper;

namespace Rosered11.OrderService.Messaging.Listener
{
    public class RestaurantApprovalResponseKafkaListener : IKafkaConsumer<RestaurantApprovalResponseModel>
    {
        private readonly ILogger<PaymentResponseKafkaListener> _logger;
        private readonly IRestaurantApprovalResponseMessageListener _restaurantApprovalResponseMessageListener;
        private readonly OrderMessagingDataMapper _orderMessagingDataMapper;
        public void Receive(List<RestaurantApprovalResponseModel> messages, List<string> keys, List<int> partitions, List<long> offsets)
        {
            _logger.LogInformation("{size} number of payment responses received with keys: {keys}, partitions: {partitions} and offsets: {offsets}",
                    messages.Count
                    , JsonSerializer.Serialize(keys)
                    , JsonSerializer.Serialize(partitions)
                    , JsonSerializer.Serialize(offsets));
            
            messages.ForEach(x => {
                if (OrderApprovalStatus.APPROVED == x.OrderApprovalStatus)
                {
                    _logger.LogInformation("Processing approved order for order id: {orderId}", x.OrderId);

                    _restaurantApprovalResponseMessageListener.OrderApproved(_orderMessagingDataMapper.ApprovalResponseModelToApprovalResponse(x));
                }
                else if (OrderApprovalStatus.REJECTED == x.OrderApprovalStatus)
                {
                    _logger.LogInformation("Processing rejected order for order id: {orderId}, with failure messages: {failurMessage}", x.OrderId, string.Join(Order.FAILURE_MESSAGE_DELIMITER, x.FailureMessages ?? new List<string>()));

                    _restaurantApprovalResponseMessageListener.OrderRejected(_orderMessagingDataMapper.ApprovalResponseModelToApprovalResponse(x));
                    
                }
            });
        }
    }
}