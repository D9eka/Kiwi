using UnityEngine;

namespace Extensions
{
    public static class TransformExtensions
    {
        public static float GetAngleToMouse(this Transform transform)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            Vector3 aimDirection = (mousePosition - transform.position).normalized;
            return Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        }
    }
}