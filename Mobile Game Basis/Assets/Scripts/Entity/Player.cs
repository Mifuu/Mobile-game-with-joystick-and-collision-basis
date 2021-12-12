using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    
    public float health = 1f;
    public float speed = 5f;

    //debug
    public Text text1;

    //components declaration
    CPhysics physics;
    CircBound circBound;
    public Joystick joystick;

    void Awake() {
        circBound = GetComponent<CircBound>();
        physics = GetComponent<CPhysics>();
    }

    void Update() {
        Move(joystick.output.x, joystick.output.y);
        CheckForEnemyCollision();
        SetWeaponTarget();
    }

    void Move(float xInput, float yInput) {
        physics.velocity.x = xInput * speed;
        physics.velocity.y = yInput * speed;
    } 

    void CheckForEnemyCollision() {
        foreach (GameObject enemyGameObject in Enemy.enemyList) {
            Enemy enemy = enemyGameObject.GetComponent<Enemy>();
            if (enemy.GetComponent<CircBound>() != null) {
                bool collision = BoundCollider.Collision(circBound,enemy.GetComponent<CircBound>());
                if (collision) {Debug.Log("true");} else {Debug.Log("false");}
            } else if (enemy.GetComponent<RectBound>() != null) {
                bool collision = BoundCollider.Collision(circBound,enemy.GetComponent<RectBound>());
                if (collision) {Debug.Log("true");} else {Debug.Log("false");}
            }
        }
    }

    //find all child weapon with targetType = OwnerAssignClosestTarget and assign it or set hasTarget false
    void SetWeaponTarget() {
        //find closest enemy
        GameObject closestEnemy = Enemy.enemyList[0];
        if (closestEnemy != null) {//found atleast 1 enemy
            foreach (GameObject enemy in Enemy.enemyList) {
                if (CMath.DistanceSqr(transform.position, enemy.transform.position) < CMath.DistanceSqr(transform.position, closestEnemy.transform.position)) {
                    closestEnemy = enemy;
                }
            }
        }
        //iterate through child object
        for (int i = 0; i < transform.childCount; i++) {
            GameObject child = gameObject.transform.GetChild(i).gameObject;
            //If do not need closest enemy info from player then continue to next iteration
            if (child.GetComponent<Weapon>() == null || child.GetComponent<Weapon>().targetType != Weapon.TargetType.OwnerAssignClosestTarget) {continue;}
            Weapon childWeapon = child.GetComponent<Weapon>();
            if (closestEnemy != null) {
                childWeapon.targetPosition = closestEnemy.transform.position;
                childWeapon.hasTarget = true;
            } else {
                childWeapon.hasTarget = false;
            }

        }
    }
}
