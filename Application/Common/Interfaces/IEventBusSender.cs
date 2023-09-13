using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IEventBusSender
    {
        public Task Send<T>(T _event) where T : BaseEvent;
        public Task CloseConnection();
    }
}
