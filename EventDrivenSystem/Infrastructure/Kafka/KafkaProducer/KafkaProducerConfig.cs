using Avro.Generic;
using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;

namespace Rosered11.Kafka.KafkaProducer
{
    public class KafkaProducerConfig<K, V>
    {
        public readonly KafkaConfigData _kafkaConfigData;
        public readonly KafkaProducerConfigData _kafkaProducerConfigData;
        public KafkaProducerConfig(KafkaConfigData kafkaConfigData, KafkaProducerConfigData kafkaProducerConfigData)
        {
            _kafkaConfigData = kafkaConfigData;
            _kafkaProducerConfigData = kafkaProducerConfigData;
        }

        public ProducerConfig ProduceConfig()
        {
            var config = new ProducerConfig();
            config.BootstrapServers = _kafkaConfigData.BootStrapServer;
            config.BatchSize = _kafkaProducerConfigData.BatchSize * _kafkaProducerConfigData.BatchSizeBoostFactor;
            config.LingerMs = _kafkaProducerConfigData.LingerMs;
            config.CompressionType = CompressionType.None;// _kafkaProducerConfigData.CompressionType;
            config.Acks = Acks.None;// _kafkaProducerConfigData.Acks;
            config.RequestTimeoutMs = _kafkaProducerConfigData.RequestTimeoutMs;
            config.MessageSendMaxRetries = _kafkaProducerConfigData.RetryCount;
            
            // using var schemaRegistry = new CachedSchemaRegistryClient(new SchemaRegistryConfig { Url = "" });

            // using (var producer = new ProducerBuilder<Null, GenericRecord>(config)
            //     .SetValueSerializer(new AvroSerializer<GenericRecord>(schemaRegistry))
            //     .Build())
            // {
            //     producer.ProduceAsync("weblog", new Message<Null, GenericRecord> { Value="a log message" });
            // }
            return config;
        }

        public ProducerBuilder<K, V> ProducerBuilder()
        {
            return new (ProduceConfig());
        }

        public IProducer<K, V> KafkaTemplate()
        {
            return ProducerBuilder().Build();
        }
    }
}