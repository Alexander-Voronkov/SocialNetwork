namespace Application.Common.Exceptions
{
    public class FriendshipNotFoundException : BaseApiException
    {
        public FriendshipNotFoundException(string exception = "") : base("Friendship not found exception" + exception)
        {
        
        }
    }
}
