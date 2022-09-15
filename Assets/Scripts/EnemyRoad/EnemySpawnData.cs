using UnityEngine;

namespace EnemyRoad
{
    public class EnemySpawnData
    {
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; }
        public float Speed { get; }
        public Collider DestinationCollider { get; }

        public EnemySpawnData(Vector3 pos, Quaternion rot, float speed, Collider destColl)
        {
            Position = pos;
            Rotation = rot;
            Speed = speed;
            DestinationCollider = destColl;
        }
    }
}