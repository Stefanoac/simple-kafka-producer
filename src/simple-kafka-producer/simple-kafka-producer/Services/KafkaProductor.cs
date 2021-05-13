using Confluent.Kafka;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace simple_kafka_producer.Services
{
    public class KafkaProductor
    {
        public const String HOST = "127.0.0.1:9092";
        public const String KAFKA_TOPIC_NAME = "test-topic";
        public const int TIME_DELAY = 2000;

        public static async Task SendMessage()
        {
            Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
            var config = new ProducerConfig { BootstrapServers = HOST };

            using (var p = new ProducerBuilder<Null, string>(config).Build())
            {
                try
                {
                    var count = 0;
                    while (true)
                    {
                        var dr = await p.ProduceAsync("test-topic", new Message<Null, string> { Value = $"test: {count++}" });

                        Log.Logger.Information($"Delivered '{dr.Value}' to '{dr.TopicPartitionOffset} | {count}'");
                        Thread.Sleep(TIME_DELAY);
                    }
                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"Delivery failed: {e.Error.Reason}");
                }
            }
        }
    }
}
