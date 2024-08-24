namespace GrpcChat.Server.Domain.Core
{
    public class EventNotifier<T> : IObservable<T>
    {
        protected readonly List<IObserver<T>> _observers = [];

        public virtual IDisposable Subscribe(IObserver<T> observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);

            return new Unsubscriber<T>(_observers, observer);
        }

        public void Notify(T value)
        {
            foreach (var observer in _observers)
            {
                try
                {
                    observer.OnNext(value);
                }
                catch (Exception ex)
                {
                    observer.OnError(ex);
                }
            }
        }

        //public void EndTransmission()
        //{
        //    foreach (var observer in _observers)
        //        observer.OnCompleted();
        //    _observers.Clear();
        //}
    }
}
