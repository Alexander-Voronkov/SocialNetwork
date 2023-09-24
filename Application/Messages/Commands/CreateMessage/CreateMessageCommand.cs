using MediatR;

namespace Application.Messages.Commands.CreateMessage
{
    public class CreateMessageCommand : IRequest<int>
    {
        public int? ChatId { get; set; }
        public string? MessageBody { get; set; }
    }
}
