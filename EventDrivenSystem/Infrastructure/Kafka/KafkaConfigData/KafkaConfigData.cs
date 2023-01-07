namespace Rosered11.Kafka
{
    public class KafkaConfigData
    {
        public string BootStrapServer { get; set; }
        public string SchemaRegistryUrlKey { get; set; }
        public string SchemaRegistryUrl { get; set; }
        public int NumOfPartition { get; set; }
        public short ReplicationFactor { get; set; }
    }
}