using System;
using UnityEngine;

namespace Core.Observable
{
    public abstract class Notifier<T> : MonoBehaviour
    {
        protected event Action<T> NotifyEvent;

        public void AddObserver(IObserver<T> observer)
        {
            NotifyEvent += observer.OnNotify;
        }

        public void RemoveObserver(IObserver<T> observer)
        {
            NotifyEvent -= observer.OnNotify;
        }

        protected void NotifyObservers(T value)
        {
            NotifyEvent?.Invoke(value);
        }
    }
}