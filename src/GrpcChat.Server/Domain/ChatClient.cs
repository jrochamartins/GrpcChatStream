using GrpcChat.Server.Domain.Core;
using GrpcChat.Shared;
using System.Collections.Concurrent;

namespace GrpcChat.Server.Domain
{
    public class ChatClient(string? name = default) : EventObserver<ChatMessage>(name)
    {
        private static readonly BlockingCollection<ChatMessage> _messages = [];

        public override void OnNext(ChatMessage value) =>
            _messages.TryAdd(value);

        public async IAsyncEnumerable<ChatMessage?> ListenMessages()
        {
            foreach (var item in _messages.GetConsumingEnumerable())
            {   
                await Task.Delay(25);
                yield return item;
            }
        }
    }
}
