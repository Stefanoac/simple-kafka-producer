using Confluent.Kafka;
using simple_kafka_producer.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace simple_kafka_producer
{
    class Program
    {
        public static async Task Main()
        {
            await KafkaProductor.SendMessage();
        }
    }
}
