﻿using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RabbitMQ.Receiver
{
    class Program
    {
        static void Main(string[] args)
        {

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                //channel.QueueDeclare(queue: "Hello", durable: false, exclusive: false, autoDelete: false, arguments: null);
                channel.QueueDeclare(queue: "Publish", durable: false, exclusive: false, autoDelete: false, arguments: null);
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) => {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine("[x] Received {0}", message);
                };

                //channel.BasicConsume(queue: "Hello", autoAck: true, consumer: consumer);
                channel.BasicConsume(queue: "Publish", autoAck: false, consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                 Console.ReadLine();
            }

        }
    }
}