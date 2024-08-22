namespace GrpcChat.Server.Domain.Core
{
    public class EventObserver<T>(string? name = default) : IObserver<T>
    {
        public string? Name => name;
        public bool Completed { get; private set; } = false;

        public virtual void OnCompleted()
        {
            Completed = true;
        }

        public virtual void OnError(Exception error)
        {
        }

        public virtual void OnNext(T value)
        {
        }
    }
}
