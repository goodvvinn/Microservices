namespace PlatformService.AsyncDataServices
{
    using System.Text;
    using System.Text.Json;
    using Microsoft.Extensions.Configuration;
    using PlatformService.Dto;
    using RabbitMQ.Client;

    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageBusClient(IConfiguration configuration)
        {
            this._configuration = configuration;
            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQHost"],
                Port = int.Parse(_configuration["RabbitMQPort"])
            };

            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);

                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
                System.Console.WriteLine("\n----> Connected to MessageBus \n");
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine($"\n----> Could not connect to the message bus: {ex.Message} \n");
            }
        }

        public void PublishNewPlatform(PlatformPublishedDto platformPublishedDto)
        {
            var message = JsonSerializer.Serialize(platformPublishedDto);
            if (_connection.IsOpen)
            {
                System.Console.WriteLine("\n---> RabbitMQ connection opened, sending message...\n");
                SendMessage(message);
            }
            else
            {
                System.Console.WriteLine("\n---> RabbitMQ connection closed, not sending \n");
            }
        }

        public void Dispose()
        {
            System.Console.WriteLine("MessageBus Disposed");
            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
        }

        private void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(
                    exchange: "trigger",
                    routingKey: string.Empty,
                    basicProperties: null,
                    body: body);
            System.Console.WriteLine($"---> We have sent {message}");
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs events)
        {
            System.Console.WriteLine("\n-----> RabbitMQ connection shutdown \n");
        }
    }
}
