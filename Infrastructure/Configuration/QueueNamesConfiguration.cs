namespace Infrastructure.Configuration
{
    public class QueueNamesConfiguration
    {
        public string? CreatedMessageEventQueue { get; set; }
        public string? CreatedChatEventQueue { get; set; }
        public string? CreatedCommentEventQueue { get; set; }
        public string? CreatedFriendrequestEventQueue { get; set; }
        public string? CreatedFriendshipEventQueue { get; set; }
        public string? CreatedReactionEventQueue { get; set; }
        public string? UpdatedPostEventQueue{get;set;}
        public string? UpdatedMessageEventQueue { get; set; }
        public string? RemovedReactionEventQueue { get; set; }
        public string? RemovedMessageEventQueue { get; set; }
    }
}
