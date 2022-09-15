using Core.Observable;
using UnityEngine;

namespace EnemyRoad
{
    public class EnemyMovementManager : Notifier<float>
    {
        private void FixedUpdate()
        {
            NotifyObservers(Time.fixedDeltaTime);
        }
    }
}