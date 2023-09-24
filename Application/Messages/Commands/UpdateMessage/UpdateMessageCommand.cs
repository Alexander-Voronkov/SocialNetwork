using MediatR;

namespace Application.Messages.Commands.UpdateMessage
{
    public class UpdateMessageCommand : IRequest<int>
    {
        public int? Id { get; set; }
        public string? MessageBody { get; set; }
    }
}
