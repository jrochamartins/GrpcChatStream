using ProtoBuf;

namespace GrpcChat.Shared
{
    [ProtoContract]
    public class User
    {
        [ProtoMember(1)]
        public string? Username { get; set; }
    }
}
