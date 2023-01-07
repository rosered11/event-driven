namespace Rosered11.Kafka
{
    public class KafkaConsumerConfigData
    {
        public string KeyDeserializer { get; set; }
        public string ValueDeserializer { get; set; }
        public string AutoOffsetReset { get; set; }
        public string SpecificAvroReaderKey { get; set; }
        public string SpecificAvroReader { get; set; }
        public bool BatchListener { get; set; }
        public bool AutoStartup { get; set; }
        public int ConcurencyLevel { get; set; }
        public int SessionTimeoutMs { get; set; }
        public int HeartbeatIntervalMs { get; set; }
        public int MaxPollIntervalMs { get; set; }
        public long PollTimeoutMs { get; set; }
        public int MaxPollRecords { get; set; }
        public int MaxPartitionFetchBytesDefault { get; set; }
        public int MaxPartitionFetchBytesBoostFactor { get; set; }
    }
}