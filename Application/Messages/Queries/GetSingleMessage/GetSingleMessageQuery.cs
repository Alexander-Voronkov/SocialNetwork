using MediatR;

namespace Application.Messages.Queries.GetSingleMessage
{
    public class GetSingleMessageQuery : IRequest<MessageDto>
    {
        public int? MessageId { get; set; }
    }
}
