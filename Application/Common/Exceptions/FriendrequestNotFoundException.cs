namespace Application.Common.Exceptions
{
    public class FriendrequestNotFoundException : BaseApiException
    {
        public FriendrequestNotFoundException(string exception = "") : base("Friendrequest not found " + exception)
        {

        }
    }
}
