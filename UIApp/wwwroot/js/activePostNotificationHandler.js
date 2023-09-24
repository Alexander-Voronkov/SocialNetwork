const activePostNotificationHub = new signalR.HubConnectionBuilder()
    .withUrl("/notifications/activePosts")
    .build();

activePostNotificationHub.on('ReceivePost', function (user, postDto) {
    const post = JSON.parse(postDto)
    const userId = +user
    updatePost(post)
})

function updatePost(post) {
    $(`.post'${post.Id} .title`).text(post.Title)
    $(`.post'${post.Id} .description`).text(post.Description)
    $(`.post'${post.Id} .body`).html(post.Body)
    $(`.post${post.Id} .tags`).empty()
    for (let i = 0; i < post.Tags.length; i++) {
        $(`.post${post.Id} .tags`).append(`<span class="tag mx-1" >${post.Tags[i]}</span>`);
    }
}

activePostNotificationHub.start()
    .then(() => {
        console.log("active posts hub connected.")
        const postIdArray = []
        document.querySelectorAll('.postIdValues').forEach(x => {
            const postId = x.value
            postIdArray.push(postId)
        })
        activePostNotificationHub.invoke('Ack', JSON.stringify(postIdArray))
    })
    .catch(error => {
        console.error(`Error while connecting to active posts hub: ${error}`)
    })