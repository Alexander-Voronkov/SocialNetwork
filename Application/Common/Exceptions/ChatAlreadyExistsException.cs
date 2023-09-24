namespace Application.Common.Exceptions
{
    public class ChatAlreadyExistsException : BaseApiException
    {
        public ChatAlreadyExistsException(string exception = "") : base("Chat already exists " + exception) 
        {
            
        }
    }
}
