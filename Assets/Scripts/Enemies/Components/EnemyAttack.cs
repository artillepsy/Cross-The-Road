using Player;
using UnityEngine;
using Zenject;

namespace Enemies.Components
{
    [RequireComponent(typeof(Enemy))]
    public class EnemyAttack : MonoBehaviour
    {
        private PlayerComponents _player;
        
        [Inject]
        public void Construct(PlayerComponents player) => _player = player;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other == _player.Collider)
            {
                _player.Health.Kill();
            }
        }
    }
}