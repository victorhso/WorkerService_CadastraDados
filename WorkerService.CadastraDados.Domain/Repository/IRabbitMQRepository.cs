using RabbitMQ.Client.Events;

namespace WorkerService.CadastraDados.Domain.Repository
{
    public interface IRabbitMQRepository
    {
        public string ReadFromQueue(EventHandler<BasicDeliverEventArgs> consumeEvent, string queue);
        public void CloseConnection();
    }
}
