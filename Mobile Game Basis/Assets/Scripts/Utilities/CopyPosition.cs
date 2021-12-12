using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyPosition : MonoBehaviour
{
    public GameObject target;

    //update variable
    Vector3 _position;

    void Awake() {
        _position = transform.position;
    }

    void Update() {
        if (target == null) {return;}
        _position.x = target.transform.position.x;
        _position.y = target.transform.position.y;
        transform.position = _position;
    }
}
