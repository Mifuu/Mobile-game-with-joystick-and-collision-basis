using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public CircBound spawnCircBound;
    public float spawnInterval = 1;//in second(s)
    public float spawnZ = -1;

    int spawnCount = 0;
    float time = 2f;

    public GameObject target;
    public GameObject enemy;

    //update variable
    Vector3 spawnLocation;

    void Update() {
        time += Time.deltaTime;//update time
        while (time/spawnInterval > spawnCount) {
            SpawnEnemy(enemy);
            spawnCount++;
        }
    }

    void SpawnEnemy(GameObject enemy) {
        float random = Random.Range(0f, 360f);
        random *= Mathf.Deg2Rad;
        spawnLocation.x = spawnCircBound.center.x + spawnCircBound.radius * Mathf.Cos(random);
        spawnLocation.y = spawnCircBound.center.y + spawnCircBound.radius * Mathf.Sin(random);
        spawnLocation.z = spawnZ;
        GameObject instance = Instantiate(enemy, spawnLocation, transform.rotation);
        instance.GetComponent<Enemy>().target = target;
    }
}
