namespace Application.Common.Exceptions
{
    public class ChatNotFoundException : BaseApiException
    {
        public ChatNotFoundException(string exception = "") :  base("Chat not found " + exception) 
        {
        
        }
    }
}
