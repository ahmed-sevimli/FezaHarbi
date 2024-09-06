using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject enemyPrefab;
    public Transform enemies;

    void Start()
    {
        for(int i=0;i<spawnPoints.Length;i++)
        {
            GameObject enemy = Instantiate(enemyPrefab,spawnPoints[i].position,Quaternion.identity,enemies);
            enemy.transform.rotation = spawnPoints[i].rotation;
            enemy.GetComponent<Enemy>().Init();
        }
    }
}