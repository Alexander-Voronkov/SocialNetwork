namespace UIApp.Services.Interfaces
{
    public interface IRabbitQueueConsumer
    {
        public void Consume(string queueName, IRabbitQueueMessageProcessor whatToDo);
    }
}
