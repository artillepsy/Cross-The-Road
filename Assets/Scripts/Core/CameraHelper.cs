using UnityEngine;

namespace Core
{
    public static class CameraHelper
    {
        private static Camera _cam;

        public static Camera Camera
        {
            get
            {
                if (_cam == null) 
                    _cam = Camera.main;
            
                return _cam;
            }
        }
    }
}