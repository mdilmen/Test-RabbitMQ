using Newtonsoft.Json;
using RabbitMQ.Client;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Test.RabbitMQ.Gateway.Models.Offer;

namespace Test.RabbitMQ.Gateway.AsyncDataServices
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConfiguration _conf;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageBusClient(IConfiguration conf)
        {
            _conf = conf;
            //_connection = connection;
            //_channel = channel;
            var factory = new ConnectionFactory()
            {
                HostName = _conf["RabbitMQHost"],
                Port = int.Parse(_conf["RabbitMQPort"])
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "offerRequest", durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);
            _channel.QueueDeclare(queue: "offerResponse", durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

            Log.Information("--> Connected to Message Bus");

        }
        

        private void RabbitMQ_ConnectionShutdown(object? sender, ShutdownEventArgs e)
        {
            Log.Information("Connection Shutdown");
        }

        public async Task PublishNewOffer(OfferPublishedModel model)
        {
            var message = JsonConvert.SerializeObject(model);
            if (_connection.IsOpen)
            {
                Log.Information("--> RabbitMQ Connection Open.. Sending Message..");
                await SendMessageRequest(message);
                await SendMessageResponse(message);
            }
            else
            {
                Log.Information("--> RabbitMQ Connection Closed.. NOT Sending Message..");
            }
        }
        private async Task SendMessageRequest(string message)
        {
        
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(  exchange:"",
                                    routingKey: "offerRequest",                                    
                                    basicProperties: null,
                                    body: body);
            Log.Information($"--> We have send {message}");
        }
        private async Task SendMessageResponse(string message)
        {

            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: "",
                                    routingKey: "offerResponse",
                                    basicProperties: null,
                                    body: body);
            Log.Information($"--> We have send {message}");
        }
        public void Dispose()
        {
            Log.Information("--> Message Bus disposed");

            if (_connection != null)
                if (_channel != null && _channel.IsOpen)
                { 
                    _channel.Close();
                    _connection.Close();
                }

        }
    }
}
