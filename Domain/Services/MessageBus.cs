using System;
using System.Text;
using System.Threading.Tasks;
using Domain.Events;
using Domain.Services.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Domain.Services
{
    public class MessageBus : IMessageBus
    {
        private IConnection _connection;
        public MessageBus()
        {
            CreateConnection();
        }

        public async Task SendClassBookingMessage(BookClassEvent message)
        {
            if (ConnectionExists().Result)
            {
                using (var channel = _connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "classBooking", durable: false, exclusive: false, autoDelete: false,
                        arguments: null);

                    var json = JsonConvert.SerializeObject(message);
                    var body = Encoding.UTF8.GetBytes(json);
                    
                    channel.BasicPublish(exchange:"", routingKey: "classBooking", basicProperties: null, body: body);
                }
            }
        }
        
        public async Task SendTrainerBookingMessage(BookTrainerEvent message)
        {
            if (ConnectionExists().Result)
            {
                using (var channel = _connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "trainerBooking", durable: false, exclusive: false, autoDelete: false,
                        arguments: null);

                    var json = JsonConvert.SerializeObject(message);
                    var body = Encoding.UTF8.GetBytes(json);
                    
                    channel.BasicPublish(exchange:"", routingKey: "trainerBooking", basicProperties: null, body: body);
                }
            }
        }

        public Task ReceiveMessage()
        {
            throw new System.NotImplementedException();
        }

        private async Task CreateConnection()
        {
            try
            {
                var factor = new ConnectionFactory()
                {
                    HostName = "localhost"
                };
                _connection = factor.CreateConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Problem connecting to RabbitMq: {ex.Message}");
            }
        }

        private async Task<bool> ConnectionExists()
        {
            if (_connection != null)
                return true;
            
            CreateConnection();
            return _connection != null; 
        }
    }
}