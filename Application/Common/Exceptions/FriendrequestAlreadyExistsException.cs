namespace Application.Common.Exceptions
{
    public class FriendrequestAlreadyExistsException : BaseApiException
    {
        public FriendrequestAlreadyExistsException(string exception = "") : base("Friendrequest already exists. " + exception)
        {

        }
    }
}
