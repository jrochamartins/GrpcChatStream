using GrpcChat.Server.Domain.Core;
using GrpcChat.Shared;

namespace GrpcChat.Server.Domain
{
    public class ChatRoom : EventNotifier<ChatMessage>
    {
        public IDisposable Subscribe(ChatClient observer)
        {
            var subscription = base.Subscribe(observer);
            Notify(new ChatMessage { Username = "Server", Content = $"{observer.Name} entrou na conversa." });
            return subscription;
        }

        public void EndTransmission(string? name)
        {
            foreach (var observer in _observers)
            {
                if (((ChatClient)observer).Name == name)
                {
                    observer.OnCompleted();
                    Notify(new ChatMessage { Username = "Server", Content = $"{name} saiu da conversa." });
                    break;
                }
            }
        }
    }
}
