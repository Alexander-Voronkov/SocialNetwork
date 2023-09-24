namespace Application.Common.Exceptions
{
    public class NotAllowedToChatException : BaseApiException
    {
        public NotAllowedToChatException(string exception = "") : base("You are not allowed to this chat! " + exception) 
        {
        
        }
    }
}
