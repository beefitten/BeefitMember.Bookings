using System;
using System.Text;
using Domain.Events;
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

        public void SendClassBookingMessage(BookClassEvent message)
        {
            if (ConnectionExists())
            {
                using (var channel = _connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "BeefitMember.ClassBookings", durable: false, exclusive: false, autoDelete: false,
                        arguments: null);

                    var json = JsonConvert.SerializeObject(message);
                    var body = Encoding.UTF8.GetBytes(json);
                    
                    channel.BasicPublish(exchange:"", routingKey: "BeefitMember.ClassBookings", basicProperties: null, body: body);
                }
            }
        }
        
        /* Is ready to publish a message to RabbitMQ  */ 
        // public async Task SendTrainerBookingMessage(BookTrainerEvent message)
        // {
        //     if (ConnectionExists())
        //     {
        //         using (var channel = _connection.CreateModel())
        //         {
        //             channel.QueueDeclare(queue: "trainerBooking", durable: false, exclusive: false, autoDelete: false,
        //                 arguments: null);
        //
        //             var json = JsonConvert.SerializeObject(message);
        //             var body = Encoding.UTF8.GetBytes(json);
        //             
        //             channel.BasicPublish(exchange:"", routingKey: "trainerBooking", basicProperties: null, body: body);
        //         }
        //     }
        // }
        //
        // public Task ReceiveMessage()
        // {
        //     throw new System.NotImplementedException();
        // }

        private void CreateConnection()
        {
            try
            {
                var factor = new ConnectionFactory
                {
                    Uri = new Uri("amqps://kxotdimk:BBCGMZtps1mW7kOqBru4qvvP5BxnfYU_@goose.rmq2.cloudamqp.com/kxotdimk")
                };
                _connection = factor.CreateConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Problem connecting to RabbitMq: {ex.Message}");
            }
        }

        private bool ConnectionExists()
        {
            if (_connection != null)
                return true;
            
            CreateConnection();
            return _connection != null; 
        }
    }
}