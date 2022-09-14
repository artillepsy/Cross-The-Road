using Core.Observable;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace Core.TouchInput
{
    public class TouchRegister : Notifier<TouchPhase>
    {
        private bool _inputEnabled = true;
        private bool _touching = false;

        public Vector2 Direction => (CurrentScreenPos - StartScreenPos).normalized;
        public Vector2 StartScreenPos { get; private set; } = Vector2.zero;

        public Vector2 CurrentScreenPos { get; private set; } = Vector2.zero;

        private void Update()
        {
            if (!_inputEnabled || Touch.activeTouches.Count == 0 || !_touching && TouchHelper.IsTouchingUI()) 
            {
                _touching = false;    
                return;
            }
            CurrentScreenPos = Touch.activeTouches[0].screenPosition;
            
            switch (Touch.activeTouches[0].phase)
            {
                case TouchPhase.Began: 
                    StartScreenPos = Touch.activeTouches[0].screenPosition;
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
            NotifyObservers(Touch.activeTouches[0].phase);
        }

        private void Awake()
        {
            EnhancedTouchSupport.Enable();
            Input.multiTouchEnabled = false;
        }
    }
}
