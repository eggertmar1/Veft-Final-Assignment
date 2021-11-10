using System;
using JustTradeIt.Software.API.Services.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;


namespace JustTradeIt.Software.API.Services.Implementations
{
    public class QueueService : IQueueService, IDisposable
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void PublishMessage(string routingKey, object body)
        {
            var msg = body.ToString();
            var bdy = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(msg));


            //create a new connection to the broker
            var factory = new ConnectionFactory() { 
                HostName = "notification-service",
                UserName = "guest",
                Password = "pass" 
                };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    // channel.QueueDeclare(queue: "hello",
                    //                      durable: false,
                    //                      exclusive: false,
                    //                      autoDelete: false,
                    //                      arguments: null);
                    
                

                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;

                    channel.BasicPublish(exchange: "order_exchange",
                                         routingKey: routingKey,
                                         basicProperties: properties,
                                         body: bdy);
                }
            }
        }
    }
}