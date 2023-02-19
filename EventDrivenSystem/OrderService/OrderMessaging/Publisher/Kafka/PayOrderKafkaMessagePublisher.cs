using System.Text.Json;
using Microsoft.Extensions.Logging;
using Rosered11.Kafka.KafkaProducer.Service;
using Rosered11.Kafka.Model;
using Rosered11.OrderService.Domain.Config;
using Rosered11.OrderService.Domain.Event;
using Rosered11.OrderService.Domain.Ports.Output.Message.Publisher.RestaurantApproval;
using Rosered11.OrderService.Messaging.Mapper;

namespace Rosered11.OrderService.Messaging.Publisher
{
    public class PayOrderKafkaMessagePublisher : OrderPaidRestaurantRequestMessagePublisher
    {
        private readonly ILogger<PayOrderKafkaMessagePublisher> _logger;
        private readonly OrderMessagingDataMapper _orderMessagingDataMapper;
        private readonly OrderServiceConfigData _orderServiceConfigData;
        private readonly KafkaProducer _kafkaProducer;
        private readonly OrderKafkaMessageHelper _orderKafkaMessageHelper;

        public PayOrderKafkaMessagePublisher(ILogger<PayOrderKafkaMessagePublisher> logger, OrderMessagingDataMapper orderMessagingDataMapper, OrderServiceConfigData orderServiceConfigData, KafkaProducer kafkaProducer
            , OrderKafkaMessageHelper orderKafkaMessageHelper)
        {
            _logger = logger;
            _orderMessagingDataMapper = orderMessagingDataMapper;
            _orderServiceConfigData = orderServiceConfigData;
            _kafkaProducer = kafkaProducer;
            _orderKafkaMessageHelper = orderKafkaMessageHelper;
        }

        public void Publish(OrderPaidEvent domainEvent)
        {
            string orderId = domainEvent.Order?.Id.GetValue().ToString() ?? string.Empty;

            try
            {
                RestaurantApprovalRequestModel restaurantApprovalRequestModel =
                _orderMessagingDataMapper.OrderPaidEventToRestaurantApprovalRequestModel(domainEvent);

                _kafkaProducer.Send(_orderServiceConfigData.RestaurantApprovalRequestTopicName
                        , orderId
                        , JsonSerializer.Serialize(restaurantApprovalRequestModel)
                        , _orderKafkaMessageHelper.GetKafkaCallback(_orderServiceConfigData.RestaurantApprovalRequestTopicName , restaurantApprovalRequestModel));
                _logger.LogInformation("RestaurantApprovalRequestModel sent to kafka for order id: {OrderId}", orderId);
            }
            catch(System.Exception ex)
            {
                _logger.LogError(ex, "Error while sending RestaurantApprovalRequestModel message to kafka with order id: {}, error: {error}", orderId, ex.Message);
            }
            
        }
    }
}