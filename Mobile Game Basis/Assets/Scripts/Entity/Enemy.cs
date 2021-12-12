using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static List<GameObject> enemyList = new List<GameObject>();

    public float health = 100;
    public float speed = 2;
    [Space(10)]
    [Tooltip("Entity Anti-Stacking distance treshold")]
    public float EasDistThreshold = 1;
    public float EasVelocity = 3;

    public GameObject target;

    //components
    CPhysics physics;

    //update
    Vector2 _velocity = new Vector2(0,0);

    void Awake() {
        enemyList.Add(this.gameObject);//Add to list
        physics = GetComponent<CPhysics>();
    }

    void Update() {
        //Follow Target
        if (target != null) {
            float followAngle = CMath.Angle(transform.position, target.transform.position);
            _velocity.x = Mathf.Cos(followAngle) * speed;
            _velocity.y = Mathf.Sin(followAngle) * speed;
            physics.velocity = _velocity;
        }

        //Entity Anti-Stacking
        foreach (GameObject enemy in enemyList) {
            if (enemy == this.gameObject) {continue;}
            if (CMath.Distance(transform.position, enemy.transform.position) <= EasDistThreshold) {
                float moveAngle = Mathf.PI + CMath.Angle(transform.position, enemy.transform.position);
                physics.velocity.x += EasVelocity * Mathf.Cos(moveAngle);
                physics.velocity.y += EasVelocity * Mathf.Sin(moveAngle);
            }
        }
    }
}
