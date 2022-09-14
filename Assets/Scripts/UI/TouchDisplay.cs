using Core.Observable;
using Core.TouchInput;
using UnityEngine;
using Zenject;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace UI
{
    public class TouchDisplay : MonoBehaviour, IObserver<TouchPhase>
    {
        [SerializeField] private RectTransform joystickCircle;
        [SerializeField] private RectTransform handler;
        private TouchRegister _touchRegister;
        private float _radius;

        [Inject]
        public void Construct(TouchRegister touchRegister) => _touchRegister = touchRegister;
        
        public void OnNotify(TouchPhase phase)
        {
            switch(phase)
            {
                case TouchPhase.Began:
                    UpdateJoystickPos(_touchRegister.StartScreenPos);
                    SetJoystickActive(true);
                    break;
                case TouchPhase.Moved:
                    UpdateHandlerPos(_touchRegister.StartScreenPos, 
                        _touchRegister.CurrentScreenPos, _touchRegister.Direction);
                    break;
                case TouchPhase.Ended:
                    SetJoystickActive(false);
                    break;
            }
        }

        private void Awake()
        {
            _radius = joystickCircle.rect.width / 2f;
            SetJoystickActive(false);
        }

        private void OnEnable() => _touchRegister.AddObserver(this);

        private void OnDisable() => _touchRegister.RemoveObserver(this);

        private void SetJoystickActive(bool status)
        {
            joystickCircle.gameObject.SetActive(status);
            handler.gameObject.SetActive(status);
        }

        private void UpdateJoystickPos(Vector2 startPos)
        {
            joystickCircle.transform.position = startPos;
            handler.transform.position = startPos;
        }

        private void UpdateHandlerPos(Vector2 startPos, Vector2 currentPos, Vector2 direction)
        {
            var pos = currentPos;
            if ((startPos - currentPos).magnitude > _radius)
            {
                pos = startPos + direction * _radius;
            }
            handler.transform.position = pos;
        }
    }
}