namespace Application.Common.Exceptions
{
    public class PostNotFoundException : Exception
    {
        public PostNotFoundException(string exception = "") : base("Post not found" + exception)
        {
        
        }
    }
}
