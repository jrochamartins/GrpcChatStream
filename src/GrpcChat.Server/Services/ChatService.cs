using GrpcChat.Server.Domain;
using GrpcChat.Shared;
using System.Runtime.CompilerServices;

namespace GrpcChat.Server.Services
{
    public class ChatService(ChatRoom chatRoom) : IChatService
    {
        public async IAsyncEnumerable<ChatMessage?> Connect(User user, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var client = new ChatClient(user.Username);
            using var subscription = chatRoom.Subscribe(client);
            await foreach (var message in client.ListenMessages())
            {
                if (client.Completed || cancellationToken.IsCancellationRequested)
                    yield break;
                yield return message;
            }
        }

        public async Task Interact(IAsyncEnumerable<Message> messages, CancellationToken cancellationToken = default)
        {
            await foreach (var message in messages)
            {
                if (cancellationToken.IsCancellationRequested)
                    break;
                chatRoom.Notify((ChatMessage)message);
            }
        }

        public async Task Disconnect(User user, CancellationToken cancelationToken = default) =>
            await Task.Run(() => chatRoom.EndTransmission(user), cancelationToken);
    }
}
