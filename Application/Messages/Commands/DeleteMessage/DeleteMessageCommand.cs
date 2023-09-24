using MediatR;

namespace Application.Messages.Commands.DeleteMessage
{
    public class DeleteMessageCommand : IRequest<int>
    {
        public int? Id { get; set; }
    }
}
