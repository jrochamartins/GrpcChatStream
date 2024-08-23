using ProtoBuf.Grpc.Configuration;

namespace GrpcChat.Shared
{
    [Service]
    public interface IChatService
    {
        IAsyncEnumerable<ChatMessage?> UserJoin(User user,
            CancellationToken cancelationToken = default);

        Task UserInteract(IAsyncEnumerable<Message> messages,
           CancellationToken cancelationToken = default);

        ValueTask UserDisconect(User user,
           CancellationToken cancelationToken = default);
    }
}
