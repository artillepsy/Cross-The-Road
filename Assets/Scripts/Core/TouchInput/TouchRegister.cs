using Core.Observable;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace Core.TouchInput
{
    public class TouchRegister : Notifier<TouchPhase>
    {
        private Vector2 _touchBeganPos = Vector2.zero;
        private Vector2 _touchCurrentPos = Vector2.zero;
        private bool _inputEnabled = true;
        private bool _touching = false;
        private Camera _camera;
    
        public Vector2 GetDirection()
        {
            var beganPos = _camera.ScreenToWorldPoint(_touchBeganPos);
            var currentPos = _camera.ScreenToWorldPoint(_touchCurrentPos);
            return (beganPos - currentPos).normalized;
        }

        private void Update()
        {
            if (!_inputEnabled || Touch.activeFingers.Count == 0 || !_touching && TouchHelper.IsTouchingUI()) 
            {
                _touching = false;    
                return;
            }
            _touchCurrentPos =  Touch.activeFingers[0].currentTouch.screenPosition;
            
            switch (Touch.activeFingers[0].currentTouch.phase)
            {
                case TouchPhase.Began: 
                    _touchBeganPos = Touch.activeFingers[0].currentTouch.screenPosition;
                    _touching = true;
                    break;
                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    _touching = true;
                    break;
                case TouchPhase.Ended:
                    _touching = false;
                    break;
                default:
                    return; // return
            }
            NotifyObservers(Touch.activeFingers[0].currentTouch.phase);
        }

        private void Awake()
        {
            EnhancedTouchSupport.Enable();
        }
    
        private void Start()
        {
            Input.multiTouchEnabled = false;
            _camera = Camera.main;
        }
    }
}
