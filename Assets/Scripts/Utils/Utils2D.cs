using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utils
{
    public static class Utils2D
    {
        public static Vector3 Vector2One = new Vector3(1, 1, 0);
        public static float Distance2(Vector3 a, Vector3 b)
        {
            a.z = 0;
            b.z = 0;
            return Vector3.Distance(a, b);
        }

        public static bool OutOfScreen(Vector3 worldPosition, float screenSizeThickness)
        {
            var screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
            var boolList = new List<bool>()
            {
                screenPosition.x > Screen.width - screenSizeThickness,
                screenPosition.x < screenSizeThickness,
                screenPosition.y > Screen.height - screenSizeThickness,
                screenPosition.y < screenSizeThickness
            };
            return boolList.Any(b => b);
        }

        public static Vector3 GetWorldPosition(Vector3 screenPosition, Camera worldCamera)
        {
            return worldCamera.ScreenToWorldPoint(screenPosition);
        }

        public static Vector3 WorldToUIPoint(Canvas canvas, Vector3 worldPoint)
        {
            var pos = Camera.main.WorldToScreenPoint(worldPoint);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, pos,
                canvas.worldCamera, out var localPoint);
            return canvas.transform.TransformPoint(localPoint);
        }
    }
}