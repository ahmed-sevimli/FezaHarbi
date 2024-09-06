using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject boxPrefab;
    public Transform cubes;
    float x, y, z;
    
    void Start()
    {
        for(int i=0;i<spawnPoints.Length;i++)
        {
            GameObject box = Instantiate(boxPrefab,spawnPoints[i].position,Quaternion.identity,cubes);
            x=Random.Range(0,90);
            y=Random.Range(0,90);
            z=Random.Range(0,90);
            box.transform.rotation=Quaternion.Euler(x,y,z);
            box.GetComponent<Box>().Init();
        }
    }
}