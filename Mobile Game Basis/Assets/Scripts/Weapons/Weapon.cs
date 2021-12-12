using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
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

    public virtual void Awake() {
        damage = baseDamage;
        range = baseRange;
        firePeriod = baseFirePeriod;
    }
}
