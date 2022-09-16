using Enemies.Spawn;
using UnityEngine;

namespace Enemies.Components
{
    [RequireComponent(typeof(Enemy))]
    public abstract class EnemyComponent : MonoBehaviour
    {
        public abstract void SetData(EnemySpawnData data);
    }
}