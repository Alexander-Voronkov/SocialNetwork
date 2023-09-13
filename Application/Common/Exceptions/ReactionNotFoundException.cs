using System.Runtime.Serialization;

namespace Application.Common.Exceptions
{
    internal class ReactionNotFoundException : Exception
    {
        public ReactionNotFoundException() : base("Reaction not found")
        {
        }
    }
}