using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmosGeometry
{
    public static void DrawCircle(float radius, Color color, Vector3 center, Vector3 normal)
    {
        Gizmos.color = color;
        Vector3 up = (normal == Vector3.up || normal == Vector3.down) ? Vector3.forward : Vector3.up;
        Vector3 right = Vector3.Cross(normal, up).normalized;
        up = Vector3.Cross(right, normal).normalized;

        float theta = 0f;
        Vector3 point = center + radius * (right * Mathf.Cos(theta) + up * Mathf.Sin(theta));

        int segments = 360;
        float deltaTheta = (2f * Mathf.PI) / segments;

        for (int i = 0; i <= segments; i++)
        {
            theta = i * deltaTheta;
            Vector3 nextPoint = center + radius * (right * Mathf.Cos(theta) + up * Mathf.Sin(theta));
            Gizmos.DrawLine(point, nextPoint);
            point = nextPoint;
        }

    }
}
