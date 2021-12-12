using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircBound : Bound
{
    public float radius;

    public CircBound(Vector2 center, float radius) {
        this.center = center;
        this.radius = radius;
    }
}
