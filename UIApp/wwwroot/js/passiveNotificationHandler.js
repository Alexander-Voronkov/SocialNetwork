const passiveFriendrequestsHubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/notifications/passiveFriendrequests")
    .build();

const passiveCommentsHubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/notifications/passiveComments")
    .build();

const passiveMessagesHubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/notifications/passiveMessages")
    .build();

const passiveReactionsHubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/notifications/passiveReactions")
    .build();

passiveMessagesHubConnection.on('ReceiveChatMessage', function (userid, messageDto) {
    const user = +userid
    const message = JSON.parse(messageDto)
    console.log(message)
    toastr.info(`You\'ve got a new message from \'${message.Owner.Username}\' !`)
})

passiveReactionsHubConnection.on('ReceiveReaction', function (userid, reactionDto) {
    const user = +userid
    const reaction = JSON.parse(reactionDto)
    toastr.info(`You\'ve got a new reaction on a post named \'${reaction.Post.Title}\' !`)
})

passiveFriendrequestsHubConnection.on('ReceiveFriendrequest', function (userid, friendrequestDto) {
    const friendrequest = JSON.parse(friendrequestDto)
    if (friendrequest.ToUserId)
        toastr.info(`You\'ve got a new friendrequest from ${friendrequest.From.Username}! `)
    else
        toastr.info(`You are now friends with ${friendrequest.SecondUser.Username}! `)
})

passiveCommentsHubConnection.on('ReceiveComment', function (userid, commentDto) {
    const user = +userid
    const comment = JSON.parse(commentDto)
    toastr.info(`You\'ve got a new comment on a post named \'${comment.Post.Title}\' !`)
})


passiveReactionsHubConnection.start()
    .then(() => {
        console.log("passive reaction hub connected.")
    })
    .catch(error => {
        console.error(`Error while connecting to passive reactions hub: ${error}`)
    })

passiveFriendrequestsHubConnection.start()
    .then(() => {
        console.log("passive friendrequests hub connected.")
    })
    .catch(error => {
        console.error(`Error while connecting to passive friendrequests hub : ${error}`)
    })

passiveCommentsHubConnection.start()
    .then(() => {
        console.log("passive comments hub connected.")
    })
    .catch(error => {
        console.error(`Error while connecting to passive comments hub : ${error}`)
    })

passiveMessagesHubConnection.start()
    .then(() => {
        console.log("passive messages hub connected.")
    })
    .catch(error => {
        console.error(`Error while connecting to passive messages hub : ${error}`)
    })



