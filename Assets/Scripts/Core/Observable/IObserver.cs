namespace Core.Observable
{
    public interface IObserver<T>
    {
        public void OnNotify(T value);
    }
}