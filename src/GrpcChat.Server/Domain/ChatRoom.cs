using GrpcChat.Server.Domain.Core;
using GrpcChat.Shared;

namespace GrpcChat.Server.Domain
{
    public class ChatRoom : EventNotifier<ChatMessage?>
    {
        public IDisposable Subscribe(ChatClient? observer)
        {
            ArgumentNullException.ThrowIfNull(observer);
            var subscription = base.Subscribe(observer);
            Notify((ChatMessage?)$"{observer.Name} entrou na conversa.");
            return subscription;
        }

        public void EndTransmission(User? user)
        {
            var observer = _observers.FirstOrDefault(c => ((ChatClient)c).Name == user?.Username);

            if (observer == null)
                return;

            observer.OnCompleted();
            Notify((ChatMessage?)$"{user?.Username} saiu da conversa.");
        }
    }
}
