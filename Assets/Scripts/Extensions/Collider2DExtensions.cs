using UnityEngine;

namespace Assets.Scripts.Extensions
{
    public static class Collider2DExtensions
    {
        public static float GetOffset(this Collider2D collider)
        {
            if (collider is CircleCollider2D circleCollider)
                return circleCollider.radius + circleCollider.offset.y;
            if (collider is BoxCollider2D boxCollider)
                return boxCollider.size.y + boxCollider.offset.y;
            if (collider is CapsuleCollider2D capsuleCollider)
                return capsuleCollider.size.y + capsuleCollider.offset.y;
            return collider.offset.y;
        }
    }
}
