const chatHubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/notifications/activeMessages")
    .build()

const chatId = $('#chatId').val()

chatHubConnection.on('ReceiveChatMessage', function (userid, messageDto) {
   

    const message = JSON.parse(messageDto)
    const user = +userid
    $('.messageContainer').append(`<div tag="${message.Id}" class="${(message.OwnerId === user) ? 'myMessageContainer' : 'othersMessageContainer'}"><div tag="${message.Id}" class="${(message.OwnerId === user) ? 'myMessage' : 'othersMessage'}" id="message${message.Id}">${message.MessageBody}</div></div>`) 
    $('#message'+message.Id).on('mousedown', function (e) {
        contextedMessageId = $(e.target).attr('tag')
    })
})

chatHubConnection.on('UpdateChatMessage', function (userid, messageDto) {
    const message = JSON.parse(messageDto)
    const user = +userid
    $('#message'+message.Id).text(message.MessageBody)
})

chatHubConnection.on('RemoveChatMessage', function (userid, messageDto) {
    const message = JSON.parse(messageDto)
    const user = +userid
    $(`div[tag=${message.Id}]`).remove()
})

chatHubConnection.start()
    .then(() => {
        console.log("active chat hub connected.")
        $('#sendMessage').click(function (event) {
            chatHubConnection.invoke('Send', chatId, $('#messageInput').val())
        })

        chatHubConnection.invoke('Ack', chatId)
    })
    .catch(error => {
        console.error(`: ${error}`);
    })

let contextedMessageId = '0';

$(function () {
    $('.myMessage').on('mousedown', function (e) {
        contextedMessageId = $(e.target).attr('tag')
    })

    $.contextMenu({
        selector: '.myMessage',
        items: {
            key: {
                name: "Edit",
                callback: function (key, opt, e) {
                    const newMessageBody = prompt('Enter new message')
                    chatHubConnection.invoke('Update', contextedMessageId, newMessageBody)
                }
            },
            sep1: "---------",
            key1: {
                name: "Delete",
                callback: function (key, opt, e) {
                    console.log(contextedMessageId)
                    chatHubConnection.invoke('Remove', contextedMessageId)
                }
            },
        },
        events: {
            show: function (opt) {
                // this is the trigger element
                var $this = this;
                // import states from data store 
                $.contextMenu.setInputValues(opt, $this.data());
                // this basically fills the input commands from an object
                // like {name: "foo", yesno: true, radio: "3", &hellip;}
            },
            hide: function (opt) {
                // this is the trigger element
                var $this = this;
                // export states to data store
                $.contextMenu.getInputValues(opt, $this.data());
                // this basically dumps the input commands' values to an object
                // like {name: "foo", yesno: true, radio: "3", &hellip;}
            }
        }
    })
})







