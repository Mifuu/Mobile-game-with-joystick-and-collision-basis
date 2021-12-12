using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectBound : Bound
{
    public Vector2 extents;
    [Tooltip("Whether to apply rotation acordding to GameObject when check for collision")]
    public bool applyRotation = false;

    public RectBound(Vector2 center, Vector2 extents) {
        this.center = center;
        this.extents = extents;
    }

    public RectBound(Vector2 center, Vector2 extents, bool applyRotation) {
        this.center = center;
        this.extents = extents;
        this.applyRotation = applyRotation;
    }
}
