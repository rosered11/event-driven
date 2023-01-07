namespace Rosered11.Kafka.KafkaProducer.Service
{
    public interface IKafkaProducer
    {
        void Send(string topicName, string key, string value, Action callback);
    }
}