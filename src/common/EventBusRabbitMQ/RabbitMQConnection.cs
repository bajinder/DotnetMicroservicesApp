using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventBusRabbitMQ
{
    public class RabbitMQConnection : IRabbitMQConnection
    {
        private IConnectionFactory _connectionFactory;
        private IConnection _connection;
        private bool _dispose;

        public bool IsConnected 
        {
            get
            {
                return _connection != null && _connection.IsOpen && !_dispose;
            }
        }

        public RabbitMQConnection(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
            if (!IsConnected) TryConnect();
        }

        public IModel CreateModel()
        {
            if (!IsConnected) throw new InvalidOperationException("No rabbit connection");

            return _connection.CreateModel();
        }

        public void Dispose()
        {
            if (_dispose) return;
            try
            {
                _connection.Dispose();
                _dispose = true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool TryConnect()
        {
            try
            {
                _connection = _connectionFactory.CreateConnection();
                _dispose = false;

            }
            catch (BrokerUnreachableException)
            {
                Thread.Sleep(2000);
                _connection = _connectionFactory.CreateConnection();
            }

            return IsConnected;
        }
    }
}
