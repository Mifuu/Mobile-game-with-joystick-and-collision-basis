using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CMath
{
    /// Calculate angle from 2 Vector2 points
    public static float Angle(Vector2 startPoint, Vector2 endPoint) {
        Vector2 deltaPosition = new Vector2(endPoint.x - startPoint.x, endPoint.y - startPoint.y);
        float angle = Mathf.Atan2(deltaPosition.y, deltaPosition.x);
        return angle;
    }

    public static float Distance(Vector2 startPoint, Vector2 endPoint) {
        Vector2 deltaPosition = new Vector2(endPoint.x - startPoint.x, endPoint.y - startPoint.y);
        return Mathf.Sqrt(Vector2.SqrMagnitude(deltaPosition));
    }

    public static float Distance(float x1, float y1, float x2, float y2) {
        return Mathf.Sqrt(Mathf.Pow(x2-x1,2) + Mathf.Pow(y2-y1,2));
    }

    public static float Distance(float distanceX, float distanceY) {
        return Mathf.Sqrt(Mathf.Pow(distanceX,2) + Mathf.Pow(distanceY,2));
    }

    public static float DistanceSqr(Vector2 startPoint, Vector2 endPoint) {
        return Mathf.Pow(endPoint.x-startPoint.x, 2) + Mathf.Pow(endPoint.y-startPoint.y, 2);
    }
}
