namespace Rosered11.Kafka.KafkaConsumer
{
    public interface IKafkaConsumer<T>
    {
        void Receive(List<T> messages, List<string> keys, List<int> partitions, List<long> offsets);
    }
}