using System.Text.Json;
using Microsoft.Extensions.Logging;
using Rosered11.Kafka.KafkaConsumer;
using Rosered11.Kafka.Model;
using Rosered11.OrderService.Common.ValueObject;
using Rosered11.OrderService.Domain;
using Rosered11.OrderService.Domain.Ports.Input.Message.Listener.Payment;
using Rosered11.OrderService.Messaging.Mapper;

namespace Rosered11.OrderService.Messaging.Listener
{
    public class PaymentResponseKafkaListener : IKafkaConsumer<PaymentResponseModel>
    {
        private readonly ILogger<PaymentResponseKafkaListener> _logger;
        private readonly IPaymentResponseMessageListener _paymentResponseMessageListener;
        private readonly OrderMessagingDataMapper _orderMessagingDataMapper;

        public PaymentResponseKafkaListener(ILogger<PaymentResponseKafkaListener> logger, PaymentResposeMessageListener paymentResponseMessageListener, OrderMessagingDataMapper orderMessagingDataMapper)
        {
            _logger = logger;
            _paymentResponseMessageListener = paymentResponseMessageListener;
            _orderMessagingDataMapper = orderMessagingDataMapper;
        }

        public void Receive(List<PaymentResponseModel> messages, List<string> keys, List<int> partitions, List<long> offsets)
        {
            _logger.LogInformation("{size} number of payment responses received with keys: {keys}, partitions: {partitions} and offsets: {offsets}",
                    messages.Count
                    , JsonSerializer.Serialize(keys)
                    , JsonSerializer.Serialize(partitions)
                    , JsonSerializer.Serialize(offsets));

            messages.ForEach(x => {
                if (PaymentStatus.COMPLETED == x.PaymentStatus)
                {
                    _logger.LogInformation("Processing successful payment for order id: {orderId}", x.OrderId);
                    _paymentResponseMessageListener.PaymentCompleted(_orderMessagingDataMapper.PaymentResponseModelToPaymentResponse(x));
                }
                else if(PaymentStatus.CANCELLED == x.PaymentStatus
                        || PaymentStatus.FAILED == x.PaymentStatus)
                {
                    _logger.LogInformation("Processing unsuccessful payment for order id: {orderId}", x.OrderId);
                    _paymentResponseMessageListener.PaymentCancelled(_orderMessagingDataMapper.PaymentResponseModelToPaymentResponse(x));
                }
            });
        }
    }
}