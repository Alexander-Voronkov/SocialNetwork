namespace Application.Common.Exceptions
{
    public class CommentNotFoundException : BaseApiException
    {
        public CommentNotFoundException(string exception = "") : base("Comment not found " + exception) 
        {
        
        }
    }
}
