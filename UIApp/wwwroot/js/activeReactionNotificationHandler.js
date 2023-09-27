const activeReactionHubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/notifications/activeReactions")
    .build()

activeReactionHubConnection.on('ReceiveReaction', function (user, reactionDto) {
    const reaction = JSON.parse(reactionDto)
    if(user === reaction.OwnerId)
        document.querySelector(`.post${reaction.PostId} .react[tag="${reaction.Type}"]`).classList.add('my')
    $(`.post${reaction.PostId} .count${reaction.Type}`).text((+$(`.post${reaction.PostId} .count${reaction.Type}`).text()) + 1)
})

activeReactionHubConnection.on('RemoveReaction', function (user, reactionDto) {
    const reaction = JSON.parse(reactionDto)
    if(user === reaction.OwnerId)
        document.querySelector(`.post${reaction.PostId} .react.my`).classList.remove('my')
    $(`.post${reaction.PostId} .count${reaction.Type}`).text((+$(`.post${reaction.PostId} .count${reaction.Type}`).text())-1)
})

$('.post .react').click(function (e) {
    const reactionType = e.target.getAttribute('tag')
    const postId = $(e.target).parent().parent().parent().attr('tag')
    activeReactionHubConnection.invoke('SendReaction', postId, reactionType)
})

activeReactionHubConnection.start()
    .then(() => {
        console.log("active reaction hub connected.")
    })
    .catch(error => {
        console.error(`Error while connecting to active reaction hub: ${error}`)
    })