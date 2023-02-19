using System.Text.Json;
using Microsoft.Extensions.Logging;
using Rosered11.Kafka.KafkaProducer.Service;
using Rosered11.Kafka.Model;
using Rosered11.OrderService.Domain.Config;
using Rosered11.OrderService.Domain.Event;
using Rosered11.OrderService.Domain.Ports.Output.Message.Publisher.Payment;
using Rosered11.OrderService.Messaging.Mapper;

namespace Rosered11.OrderService.Messaging.Publisher
{
    public class CreateOrderKafkaMessagePublisher : OrderCreatedPaymentRequestMessagePublisher
    {
        private readonly ILogger<CreateOrderKafkaMessagePublisher> _logger;
        private readonly OrderMessagingDataMapper _orderMessagingDataMapper;
        private readonly OrderServiceConfigData _orderServiceConfigData;
        private readonly KafkaProducer _kafkaProducer;
        private readonly OrderKafkaMessageHelper _orderKafkaMessageHelper;

        public CreateOrderKafkaMessagePublisher(
            ILogger<CreateOrderKafkaMessagePublisher> logger
            , OrderMessagingDataMapper orderMessagingDataMapper
            , OrderServiceConfigData orderServiceConfigData
            , KafkaProducer kafkaProducer
            , OrderKafkaMessageHelper orderKafkaMessageHelper)
        {
            _logger = logger;
            _orderMessagingDataMapper = orderMessagingDataMapper;
            _orderServiceConfigData = orderServiceConfigData;
            _kafkaProducer = kafkaProducer;
            _orderKafkaMessageHelper = orderKafkaMessageHelper;
        }
        public void Publish(OrderCreatedEvent domainEvent)
        {
            string orderId = domainEvent.Order?.Id.GetValue().ToString() ?? string.Empty;
            try
            {
                _logger.LogInformation("Received OrderCreatedEvent for order id: {OrderId}", orderId);

                PaymentRequestModel paymentRequestModel = _orderMessagingDataMapper
                    .OrderCreatedEventToPaymentRequestModel(domainEvent);

                _kafkaProducer.Send(_orderServiceConfigData.PaymentRequestTopicName
                        , orderId
                        , JsonSerializer.Serialize(paymentRequestModel)
                        , _orderKafkaMessageHelper.GetKafkaCallback(_orderServiceConfigData.PaymentResponseTopicName, paymentRequestModel));

                _logger.LogInformation("PaymentRequestModel sent to kafka for order id: {orderId}", paymentRequestModel.OrderId);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error while sending PaymentRequestModel message to kafka with order id: {}, error: {error}", orderId, ex.Message);
            }
            
        }

        
    }
}