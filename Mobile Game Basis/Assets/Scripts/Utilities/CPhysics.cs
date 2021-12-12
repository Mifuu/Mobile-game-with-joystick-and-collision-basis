using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPhysics : MonoBehaviour
{
    [Tooltip("Initial velocity")]
    public Vector2 velocity;
    [Tooltip("Initial acceleration")]
    public Vector2 acceleration;
    [Tooltip("Initial angular velocity")]
    public float angularVelocity;
    [Tooltip("Initial angular acceleration")]
    public float angularAcceleration;
    [Tooltip("Initial local velocity (dependent on rotation)")]
    public Vector2 localVelocity;

    void Update() {
        //using Time.deltaTime for realtime
        
        //global velocity and position calculation using transform.position directly
        velocity += acceleration * Time.deltaTime;
        Vector2 pos = transform.position;
        pos += velocity * Time.deltaTime;
        transform.position = pos;
        
        //local position calculation using transform.Translate
        transform.Translate(localVelocity * Time.deltaTime);

        //rotation
        angularVelocity += angularAcceleration * Time.deltaTime;
        transform.Rotate(new Vector3(0,0,angularVelocity));
    }
}
