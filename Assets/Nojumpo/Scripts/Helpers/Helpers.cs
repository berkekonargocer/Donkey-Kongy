using UnityEngine;

namespace Nojumpo.Helper
{
    public static class Helpers
    {

        private static Camera _camera;

        public static Camera MainCamera
        {
            get
            {
                if (_camera == null)
                    _camera = Camera.main;

                return _camera;
            }
        }
        public static Vector2 GetWorldPositionOfCanvasElement(RectTransform element)
        {
            RectTransformUtility.ScreenPointToWorldPointInRectangle(element, element.position, MainCamera, out var result);
            return result;
        }
    }
}