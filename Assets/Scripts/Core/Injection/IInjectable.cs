namespace Core.Injection
{
    public interface IInjectable<T>
    {
        public void Construct(T value);
    }
}