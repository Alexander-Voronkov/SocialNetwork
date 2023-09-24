namespace Application.Common.Exceptions
{
    public class BaseApiException : Exception
    {
        public BaseApiException(string exception = "") : base("Api exception " + exception) 
        {
        
        }
    }
}
