using Rosered11.Kafka.Model;

namespace Rosered11.OrderService.Messaging.Publisher
{
    public class OrderKafkaMessageHelper
    {
        public Action GetKafkaCallback<T>(string paymentResponseTopicName, T paymentRequestModel)
        {
            // Implement callback kafka
            return () => {};
        }
    }
}