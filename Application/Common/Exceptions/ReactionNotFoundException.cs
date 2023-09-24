namespace Application.Common.Exceptions
{
    internal class ReactionNotFoundException : BaseApiException
    {
        public ReactionNotFoundException(string exception = "") : base("Reaction not found" + exception)
        {
        }
    }
}