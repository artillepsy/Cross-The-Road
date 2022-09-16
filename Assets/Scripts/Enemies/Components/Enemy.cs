using System.Collections.Generic;
using Enemies.Spawn;
using UnityEngine;

namespace Enemies.Components
{
    public class Enemy : EnemyComponent
    {
        [SerializeField] private List<EnemyComponent> components;

        public override void SetData(EnemySpawnData data)
        {
            components.ForEach(c => c.SetData(data));
        }
    }
}