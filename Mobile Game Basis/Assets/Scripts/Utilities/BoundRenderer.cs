using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundRenderer : MonoBehaviour
{
    public BoundSet[] boundSets;

    void OnDrawGizmos() {
        foreach (BoundSet boundSet in boundSets) {
            if (boundSet.show) {
                Gizmos.color = boundSet.color;
                foreach (Bound bound in boundSet.bounds) {
                    if (bound == null || !bound.gameObject.activeInHierarchy) {}
                    else if (bound.GetType() == typeof(CircBound)) {
                        CircBound _bound = (CircBound) bound;
                        //Vector3 _position = new Vector3(_bound.center.x, _bound.center.y, transform.position.z) + _bound.transform.parent.position;
                        Vector3 _position = new Vector3(_bound.center.x, _bound.center.y, transform.position.z) + _bound.transform.position;
                        Gizmos.DrawWireSphere(_position, _bound.radius);
                    }
                    else if (bound.GetType() == typeof(RectBound)) {
                        RectBound _bound = (RectBound) bound;
                        //Vector3 _position = new Vector3(_bound.center.x, _bound.center.y, transform.position.z) + _bound.transform.parent.position;
                        Vector3 _position = new Vector3(_bound.center.x, _bound.center.y, transform.position.z) + _bound.transform.position;
                        Gizmos.DrawWireCube(_position, _bound.extents*2);
                    }
                }
            }

        }
    }
}

[System.Serializable]
public struct BoundSet {
    public string name;
    public Color color;
    public bool show;
    public Bound[] bounds;
}
