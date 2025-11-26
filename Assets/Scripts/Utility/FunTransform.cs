using Unity.VisualScripting;
using UnityEngine;

public static class FunTransform
{
    public static bool IsRendering(this Transform transform)
    {
        float outOfBoundsDistance = 1.1f;
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);

        return !(screenPoint.x < -outOfBoundsDistance || screenPoint.x > 1 + outOfBoundsDistance ||
               screenPoint.y < -outOfBoundsDistance || screenPoint.y > 1 + outOfBoundsDistance ||
               screenPoint.z < 0);
    }
    
}
