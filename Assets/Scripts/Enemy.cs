using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : Character
{
    public GameObject enemyShotPrefab;
    public static bool enemyCanShoot = true;
    internal bool enemyCanTurn = false;
    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    public void Init()
    {
        //Awake
        fire_period = 0.75f;
        eTimeAfterShoot = UnityEngine.Random.Range(0f, 0.75f);
        health = 2;
        characterShot = enemyShotPrefab;
        midHealth = Resources.Load("ball_mat1", typeof(Material)) as Material;
        lowHealth = Resources.Load("ball_mat3", typeof(Material)) as Material;
        
        //Enemy Level Control
        if(String.Equals(LevelController.Instance.GetActiveLevel(),"Level1")
        || String.Equals(LevelController.Instance.GetActiveLevel(),"Level2"))
        {
            enemyCanShoot = false;
        }
        else
        {
            enemyCanShoot = true;
            if(String.Equals(LevelController.Instance.GetActiveLevel(),"Level5")
            || String.Equals(LevelController.Instance.GetActiveLevel(),"Level6"))
            {
                enemyCanTurn = true;
                fire_period = fire_period * 2;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if(enemyCanShoot){
		    Shoot(GetComponent<EnemyMovementController>());
        }

        //Enemy Looks At Player
        if(enemyCanTurn)
        {
            try
            {
                transform.LookAt(GameObject.Find("Player").transform.position);
            }
            catch(NullReferenceException e)
            {
                Debug.Log("NullReferenceException");
            }
        }
    }
}