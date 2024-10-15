using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiPlayer : Character
{
    public GameObject shotPrefab;
    public KeyCode shootKey;

    void Awake()
    {
        fire_period = 0.20f;
        health = 10;
        characterShot = shotPrefab;
        midHealth = Resources.Load("ball_mat1", typeof(Material)) as Material;
        lowHealth = Resources.Load("ball_mat3", typeof(Material)) as Material;
    }

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if(Input.GetKey(shootKey)){
			Shoot(GetComponent<MultiPlayerMovementController>());
		}
    }
}
