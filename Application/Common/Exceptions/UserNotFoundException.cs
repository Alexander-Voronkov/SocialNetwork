namespace Application.Common.Exceptions
{
    public class UserNotFoundException : BaseApiException
    {
        public UserNotFoundException(string exception = "") : base("User not found " + exception)
        {
        }
    }
}
