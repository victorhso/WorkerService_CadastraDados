using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using WorkerService.CadastraDados.Domain.Dtos;
using WorkerService.CadastraDados.Domain.Repository;

namespace WorkerService.CadastraDados.Repository
{
    public class RabbitMQRepository : IRabbitMQRepository
    {
        public RabbitMQConfiguration _rabbitMqOptions;
        private ConnectionFactory _factory;
        private IConnection _connection;
        private IModel _entryChannel;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public RabbitMQRepository(RabbitMQConfiguration rabbitMqOptions)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            _rabbitMqOptions = rabbitMqOptions;

            _factory = new ConnectionFactory()
            {
                HostName = _rabbitMqOptions.HOST_MQ,
                Port = int.Parse(_rabbitMqOptions.PORT_MQ),
                UserName = _rabbitMqOptions.USER_NAME_MQ,
                Password = _rabbitMqOptions.PWD_MQ,
                ClientProvidedName = _rabbitMqOptions.CLIENT_PROVIDER_MQ
            };
        }

        public string ReadFromQueue(EventHandler<BasicDeliverEventArgs> consumeEvent, string queue)
        {
            string result = String.Empty;

            try
            {
                if (_connection is null)
                {
                    _connection = _factory.CreateConnection();
                }

                if (_entryChannel is null || _entryChannel.IsClosed)
                {
                    _entryChannel = _connection.CreateModel();
                    _entryChannel.ExchangeDeclare(_rabbitMqOptions.EXCHANGE_NAME_MQ, ExchangeType.Topic, true, false);

                    _entryChannel.QueueDeclare(queue, true, false, false);
                    _entryChannel.QueueBind(queue, _rabbitMqOptions.EXCHANGE_NAME_MQ, queue);
                }

                if (_connection is not null && _entryChannel is not null && _entryChannel.IsOpen)
                {
                    EventingBasicConsumer consumer = new EventingBasicConsumer(_entryChannel);

                    consumer.Received += consumeEvent;
                    _entryChannel.BasicConsume(queue, true, consumer);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public void CloseConnection()
        {
            if (_entryChannel is not null)
            {
                _entryChannel.Close();
                _entryChannel.Dispose();
                _entryChannel = null;
            }

            if (_connection is not null)
            {
                _connection.Close();
                _connection.Dispose();
                _connection = null;
            }
        }

    }
}
