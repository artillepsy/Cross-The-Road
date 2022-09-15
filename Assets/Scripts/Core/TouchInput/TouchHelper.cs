using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace Core.TouchInput
{
    public static class TouchHelper
    {
        public static bool IsTouchingUI()
        {
            var data = new PointerEventData(EventSystem.current)
            {
                position = new Vector2(Touch.activeFingers[0].currentTouch.screenPosition.x,
                    Touch.activeFingers[0].currentTouch.screenPosition.y)
            };
            var results = new List<RaycastResult>();

            EventSystem.current.RaycastAll(data, results);
            return results.Count > 0;
        }
    }
}