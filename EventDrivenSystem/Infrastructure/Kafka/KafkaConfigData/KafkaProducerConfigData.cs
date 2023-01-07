namespace Rosered11.Kafka
{
    public class KafkaProducerConfigData
    {
        public string KeySerializerClass { get; set; }
        public string ValueSerializerClass { get; set; }
        public string CompressionType { get; set; }
        public string Acks { get; set; }
        public int BatchSize { get; set; }
        public int BatchSizeBoostFactor { get; set; }
        public int LingerMs { get; set; }
        public int RequestTimeoutMs { get; set; }
        public int RetryCount { get; set; }
    }
}