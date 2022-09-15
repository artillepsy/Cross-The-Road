using System.Collections;
using Core.Observable;
using Core.TouchInput;
using UnityEngine;
using Zenject;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace Player
{
    public class PlayerMovementController : MonoBehaviour, IObserver<TouchPhase>
    {
        [SerializeField] private float speed = 5f;
        [SerializeField] private float rotationAngles = 400f;
        [SerializeField] private bool drawGizmos = true;
        
        private Quaternion _rotateTo = Quaternion.identity;
        private Vector3 _direction = Vector3.zero;
        private TouchRegister _touchRegister;
        private WaitForFixedUpdate _waitForFixedUpdate;
        private Coroutine _moveCo;
        private Rigidbody _rb;
        private Camera _cam;

        [Inject]
        public void Construct(TouchRegister register, Camera cam)
        {
            _touchRegister = register;
            _cam = cam;
        }

        public void OnNotify(TouchPhase phase)
        {
            switch (phase)
            {
                case TouchPhase.Began:
                    _moveCo = StartCoroutine(MoveCo());
                    UpdateDirectionAndRotation();
                    break;
                case TouchPhase.Moved:
                    UpdateDirectionAndRotation();
                    break;
                case TouchPhase.Ended:
                    StopCoroutine(_moveCo);
                    _moveCo = null;
                    break;
            }
        }

        private void Awake()
        {
            _waitForFixedUpdate = new WaitForFixedUpdate();
            _rb = GetComponent<Rigidbody>();
        }

        private void OnEnable() => _touchRegister.AddObserver(this);

        private void OnDisable() => _touchRegister.RemoveObserver(this);

        private void UpdateDirectionAndRotation()
        {
            var rotationY = -_cam.transform.rotation.eulerAngles.y;

            if (_touchRegister.Direction == Vector2.zero)
                return;
            
            _direction = new Vector3(_touchRegister.Direction.x, 0,
                    _touchRegister.Direction.y);
            _direction = Quaternion.Euler(0, -rotationY, 0) * _direction;
            _rotateTo = Quaternion.LookRotation(_direction, Vector3.up);
        }
        
        private IEnumerator MoveCo()
        {
            while (true)
            {
                if (_touchRegister.Direction != Vector2.zero)
                {
                    Rotate();
                    Move();
                }
                yield return _waitForFixedUpdate;
            }
        }

        private void Rotate()
        {
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation, _rotateTo, rotationAngles * Time.fixedDeltaTime);
        }

        private void Move()
        {
            _rb.MovePosition(transform.position + transform.forward * (speed * Time.fixedDeltaTime));
        }

        private void OnDrawGizmos()
        {
            if (!drawGizmos)
                return;
            
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + _direction * 3f);
        }
    }
}