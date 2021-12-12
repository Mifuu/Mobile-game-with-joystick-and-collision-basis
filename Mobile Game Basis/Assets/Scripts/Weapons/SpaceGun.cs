using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceGun : Weapon
{
    /* from super class
    public GameObject owner;
    public Vector2 targetPosition;
    public bool hasTarget = false;
    public enum TargetType{OwnerAssignClosestTarget};
    public TargetType targetType;
    public int tier = 1;
    [Space()]
    public float baseDamage = 10.0f;
    public float baseRange = 6.0f;
    public float baseFirePeriod = 1.0f;
    [Space()]
    public float damage;
    public float range;
    public float firePeriod;
    */
    public GameObject bullet;
    public int subFireCount = 1;
    public float subFirePeriod = 0;
    private float nextActionTime = 0.0f;
    
    void Update() {
        if (hasTarget && CMath.DistanceSqr(transform.position, targetPosition) < Mathf.Pow(range, 2)) {
            if (Time.time > nextActionTime) {//fire every firePeriod
                nextActionTime = Time.time + firePeriod;
                StartCoroutine(MainFire());
            }
        }
    }

    IEnumerator MainFire() {
        for (int i = 0; i < subFireCount; i++) {
            Fire();
            yield return new WaitForSeconds(subFirePeriod);
        }
    }

    void Fire() {
        if (bullet == null) {Debug.Log("SpaceGun.cs error: no bullet"); return;}
        if (hasTarget && targetPosition != null) {
            GameObject instance = Instantiate(bullet, transform.position, transform.rotation);
            Projectile projectile = instance.GetComponent<Projectile>();
            //assign velocity to instance
            float angle = CMath.Angle(transform.position, targetPosition);
            instance.GetComponent<CPhysics>().velocity.x = Mathf.Cos(angle) * projectile.speed;
            instance.GetComponent<CPhysics>().velocity.y = Mathf.Sin(angle) * projectile.speed;
        }
    }
}
