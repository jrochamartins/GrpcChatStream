using ProtoBuf;

namespace GrpcChat.Shared
{
    [ProtoContract]
    public class ChatMessage
    {
        [ProtoMember(1)]
        public string? Username { get; set; }

        [ProtoMember(2)]
        public string? Content { get; set; }
    }
}
