namespace GrpcChat.Server.Domain.Core
{
    internal class Unsubscriber<T>(List<IObserver<T>> observers, IObserver<T> observer) : IDisposable
    {
        public void Dispose()
        {
            if (observer != null && observers.Contains(observer))
                observers.Remove(observer);
        }
    }
}
