using Core.Observable;
using UnityEngine;

namespace Enemies.Movement
{
    public class EnemyMovementManager : Notifier<float>
    {
        private void FixedUpdate()
        {
            NotifyObservers(Time.fixedDeltaTime);
        }
    }
}