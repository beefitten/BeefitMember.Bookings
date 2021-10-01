using System;
using System.Text;
using Consumer.Service.Consumer;
using Consumer.Service.Handlers;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Consumer.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            // var bookingsHandler = new BookingsHandler();
            // var consumer = new MqConsumer(bookingsHandler);
            //
            // Console.ReadLine();
            
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "classBooking",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine("Received: {0}", message);
                    };
                    channel.BasicConsume(queue: "classBooking",
                        autoAck: true,
                        consumer: consumer);

                    Console.ReadLine();
                }
            }
        }
    }
}