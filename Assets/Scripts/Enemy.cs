using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : Character
{
    public bool enemyCanShoot = true;
    private bool enemyCanTurn = false;
    void Awake()
    {
        fire_period = 0.75f;
        health = 2;
        characterShot = GameObject.Find("EnemyShot");
        midHealth = Resources.Load("ball_mat1", typeof(Material)) as Material;
        lowHealth = Resources.Load("ball_mat3", typeof(Material)) as Material;
        
        //Enemy turn control
        if(String.Equals(LevelController.GetActiveLevel(),"Level5")
        || String.Equals(LevelController.GetActiveLevel(),"Level6"))
        {
            enemyCanTurn = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.SubscribeCharacterEvent(this);
        if(String.Equals(LevelController.GetActiveLevel(),"Level5") || String.Equals(LevelController.GetActiveLevel(),"Level6"))
        {
            fire_period = fire_period * 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.X))
        {
            enemyCanShoot = !enemyCanShoot;
        }

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