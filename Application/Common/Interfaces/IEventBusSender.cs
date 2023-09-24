using Domain.Common;

namespace Application.Common.Interfaces
{
    public interface IEventBusSender : IDisposable
    {
        public Task Send<T>(T _event) where T : BaseEvent;
    }
}
