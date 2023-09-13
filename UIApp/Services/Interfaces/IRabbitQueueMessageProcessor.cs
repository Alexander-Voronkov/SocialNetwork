namespace UIApp.Services.Interfaces
{
    public interface IRabbitQueueMessageProcessor
    {
        public void Process(string message);
    }
}
