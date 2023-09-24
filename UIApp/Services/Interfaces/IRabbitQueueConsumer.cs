namespace UIApp.Services.Interfaces
{
    public interface IRabbitQueueConsumer : IDisposable
    {
        public Task Consume(CancellationToken cancToken);
    }
}
