namespace MyHomeAutomationSystem.Internals;

public class EntityStates<TEntity> : IObservable<TEntity>
{
    private readonly List<IObserver<TEntity>> _observers;

    public EntityStates()
    {
        _observers = new List<IObserver<TEntity>>();
    }

    public IDisposable Subscribe(IObserver<TEntity> observer)
    {
        if (!_observers.Contains(observer))
            _observers.Add(observer);

        return new Unsubscriber(_observers, observer);
    }

    public void SetSensorValue(TEntity sensor)
    {
        foreach (var observer in _observers)
            observer.OnNext(sensor);
    }

    private class Unsubscriber : IDisposable
    {
        private readonly IObserver<TEntity>       _observer;
        private readonly List<IObserver<TEntity>> _observers;

        public Unsubscriber(List<IObserver<TEntity>> observers, IObserver<TEntity> observer)
        {
            _observers = observers;
            _observer  = observer;
        }

        public void Dispose()
        {
            if (_observer != null) _observers.Remove(_observer);
        }
    }
}