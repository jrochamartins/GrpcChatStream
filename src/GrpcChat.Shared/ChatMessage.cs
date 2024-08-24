using ProtoBuf;

namespace GrpcChat.Shared
{
    [ProtoContract]
    public class ChatMessage : Message
    {
        [ProtoMember(1)]
        public string? Username { get; set; }

        public string Render() =>
            $"[{Created:HH:mm:ss}] {Username}: {Content}";

        public static explicit operator ChatMessage?(string? content)
        {
            if (content == null)
                return default;

            return new ChatMessage()
            {
                Username = "Server",
                Content = content,
            };
        }
    }

    [ProtoContract]
    [ProtoInclude(2, typeof(ChatMessage))]
    public class Message : Entry
    {
        [ProtoMember(1)]
        public string? Content { get; set; }
    }

    public class Entry
    {
        public DateTime Created { get; } = DateTime.Now;
    }
}
