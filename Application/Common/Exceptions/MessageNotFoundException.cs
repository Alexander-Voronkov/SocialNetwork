namespace Application.Common.Exceptions
{
    public class MessageNotFoundException : BaseApiException
    {
        public MessageNotFoundException(string exception = "") :base("Message not found" + exception)
        {

        }
    }
}
