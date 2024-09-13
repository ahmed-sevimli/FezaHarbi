using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public GameObject playerShotPrefab;
    void Awake()
    {
        spawning_point = Vector3.zero;
        fire_period = 0.20f;
        health = 3;
        characterShot = playerShotPrefab;
        midHealth = Resources.Load("ball_mat1", typeof(Material)) as Material;
        lowHealth = Resources.Load("ball_mat3", typeof(Material)) as Material;
    }

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if(Input.GetKey(KeyCode.Space)){
			Shoot(GetComponent<PlayerMovementController>());
		}
    }
}
