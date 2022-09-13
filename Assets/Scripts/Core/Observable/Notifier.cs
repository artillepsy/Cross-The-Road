using System.Collections.Generic;
using UnityEngine;

namespace Core.Observable
{
    public class Notifier<T> : MonoBehaviour
    {
        private readonly List<IObserver<T>> _observers = new List<IObserver<T>>();

        public void AddObserver(IObserver<T> observer)
        {
            _observers.Add(observer);
        }

        public void RemoveObserver(IObserver<T> observer)
        {
            _observers.Remove(observer);
        }

        protected void NotifyObservers(T value)
        {
            _observers.ForEach(o => o.OnNotify(value));
        }
    }
}