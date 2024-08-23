using GrpcChat.Server.Domain;
using GrpcChat.Shared;
using System.Runtime.CompilerServices;

namespace GrpcChat.Server.Services
{
    public class ChatService(ChatRoom chatRoom) : IChatService
    {
        public async IAsyncEnumerable<ChatMessage?> UserJoin(User user,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var client = new ChatClient(user.Username);
            using var subscription = chatRoom.Subscribe(client);
            await foreach (var message in client.ListenMessages())
            {
                if (client.Completed)
                    yield break;
                yield return message;
            }
        }

        public async Task UserInteract(IAsyncEnumerable<Message> messages,
            CancellationToken cancellationToken = default)
        {
            await foreach (var message in messages)
            {
                chatRoom.Notify((ChatMessage)message);
            }
        }

        public async ValueTask UserDisconect(User user, CancellationToken cancelationToken = default)
        {
            await Task.Run(() => chatRoom.EndTransmission(user.Username), cancelationToken);
        }
    }
}
