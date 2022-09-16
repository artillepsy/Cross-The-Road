using UnityEngine;

namespace Enemies.Movement
{
    public class EnemyPath : MonoBehaviour
    {
        [SerializeField] private Transform startCorner;
        [SerializeField] private Transform endCorner;
        [SerializeField] private bool drawGizmos;

        public Vector3 StartPos => startCorner.position;
        public Collider SelfCollider { get; private set; }
        public Vector3 UnNormDirection { get; private set; }

        private void Awake()
        {
            SelfCollider = GetComponentInChildren<Collider>();
            UnNormDirection = (endCorner.position - startCorner.position);
        }

        private void OnDrawGizmos()
        {
            if (!drawGizmos)
                return;
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(startCorner.position, endCorner.position);
        }
    }
}