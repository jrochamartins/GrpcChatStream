using ProtoBuf.Grpc.Configuration;

namespace GrpcChat.Shared
{
    [Service]
    public interface IChatService
    {
        IAsyncEnumerable<ChatMessage?> Connect(User user,
            CancellationToken cancelationToken = default);

        Task Interact(IAsyncEnumerable<Message> messages,
           CancellationToken cancelationToken = default);

        Task Disconnect(User user,
           CancellationToken cancelationToken = default);
    }
}
