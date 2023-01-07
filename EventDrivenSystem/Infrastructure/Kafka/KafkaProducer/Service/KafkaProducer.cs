using Confluent.Kafka;
using Microsoft.Extensions.Logging;

namespace Rosered11.Kafka.KafkaProducer.Service
{
    public class KafkaProducer : IKafkaProducer, IDisposable
    {
        private readonly ILogger<KafkaProducer> _logger;
        private readonly IProducer<Null, string> _kafkaTemplate;
        private bool _disposed = false;

        public KafkaProducer(ILogger<KafkaProducer> logger, KafkaProducerConfig<Null, string> kafkaProducerConfig)
        {
            _logger = logger;
            _kafkaTemplate = kafkaProducerConfig.KafkaTemplate();
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _kafkaTemplate.Dispose();
                _disposed = true;
            }
        }

        public void Send(string topicName, string key, string message, Action callback)
        {
            _logger.LogInformation("Sending message={message} to topic={topic}", message, topicName);
            try
            {
                _kafkaTemplate.Produce(topicName, new Message<Null, string>{ Value = message});
            }
            catch (ProduceException<Null, string> ex)
            {
                _logger.LogError(ex, "Error on kafka producer with key: {key} and message: {message} and exception {exception}}", key, message, ex.Message);
                throw new KafkaProducerException($"Error on kafka producer with key: {key} and message: {message}");
            }
        }
    }
}