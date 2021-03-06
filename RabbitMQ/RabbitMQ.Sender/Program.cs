﻿using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMQ.Sender
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() {HostName = "localhost" };


            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {

                channel.QueueDeclare(queue: "Publish", durable:false,exclusive:false,autoDelete:false,arguments:null);
                string message = "hello my postman" + DateTime.Today.ToLongDateString();
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "", routingKey: "Publish", basicProperties: null,body:body);
                Console.WriteLine("[x] sent {0}", message);


            }
             Console.WriteLine(" Press [enter] to exit.");
             Console.ReadLine();


        }

    }
}
