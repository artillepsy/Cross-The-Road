using UnityEngine;

namespace Player
{
    public class PlayerComponents : MonoBehaviour
    {
        public Collider Collider { get; private set; }
        public PlayerHealth Health { get; private set; }

        private void Awake()
        {
            Collider = GetComponentInChildren<Collider>();
            Health = GetComponent<PlayerHealth>();
        }
    }
}