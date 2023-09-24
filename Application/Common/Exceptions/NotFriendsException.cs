namespace Application.Common.Exceptions
{
    public class NotFriendsException : BaseApiException
    {
        public NotFriendsException(string exception = "") : base("Users are not friends " + exception) 
        {
        
        }
    }
}
