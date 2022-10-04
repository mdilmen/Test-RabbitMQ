using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Test.RabbitMQ.OfferService.EventProcessing;

namespace Test.RabbitMQ.OfferService.AsyncDataServices
{
    public class MessageBusSubscriber : BackgroundService
    {
        private readonly IConfiguration _conf;
        private readonly IEventProcessor _eventProcessor;        
        private IConnection _connection;
        private IModel _channel;

        public MessageBusSubscriber(IConfiguration conf, IEventProcessor eventProcessor)
        {
            _conf = conf;
            _eventProcessor = eventProcessor;
            
            InitializeRabbitMQ();            
        }

        private Task InitializeRabbitMQ()
        {            
            var factory = new ConnectionFactory()
            {
                HostName = _conf["RabbitMQHost"],
                Port = int.Parse(_conf["RabbitMQPort"])
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            // Gateway 
            _channel.ExchangeDeclare(exchange: "Offer", type: ExchangeType.Direct);
            _channel.QueueDeclare(queue: "OfferResponse", durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

            _channel.QueueBind("OfferResponse", "Offer", "offerResponse");


            _channel.QueueDeclare(queue: "OfferRequest", durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

            Log.Information("--> Listening to Message Bus");
            return Task.CompletedTask;
        }

        private void RabbitMQ_ConnectionShutdown(object? sender, ShutdownEventArgs e)
        {
            Log.Information("Connection Shutdown");
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ModuleHandle, ea) =>
            {
                Log.Information("--> Event Received!!");

                var body = ea.Body;
                var notificationMessage = Encoding.UTF8.GetString(body.ToArray());

                _eventProcessor.ProcessEvent(notificationMessage);
            };
            _channel.BasicConsume(queue: "OfferRequest", autoAck: true, consumer: consumer);
            return Task.CompletedTask;
        }
    }
}
