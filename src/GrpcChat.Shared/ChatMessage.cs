using ProtoBuf;

namespace GrpcChat.Shared
{
    [ProtoContract]
    public class ChatMessage : Message
    {
        [ProtoMember(1)]
        public string? Username { get; set; }

        public override string Render() => 
            $"{Username}: {base.Render()}";
    }

    [ProtoContract]
    [ProtoInclude(2, typeof(ChatMessage))]
    public class Message
    {
        [ProtoMember(1)]
        public string? Content { get; set; }

        public virtual string Render() => 
            $"{Content}";
    }
}
