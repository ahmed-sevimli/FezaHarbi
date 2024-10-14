using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    // Start is called before the first frame update
    private Rigidbody bossBody;
    private float speed_force = 1000.0f;
    private float max_speed = 20f;

    void Start()
    {
        base.Start();
        bossBody = ownerBody;
        enemyCanTurn = true;
        //Init
        fire_period = 0.80f;
        health = 10;
        characterShot = enemyShotPrefab;
        midHealth = Resources.Load("ball_mat1", typeof(Material)) as Material;
        lowHealth = Resources.Load("ball_mat3", typeof(Material)) as Material;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        Accelerate();
        if (enemyCanShoot)
        {
            Shoot(GetComponent<EnemyMovementController>());
        }

        //Enemy Looks At Player
        if (enemyCanTurn)
        {
            try
            {
                transform.LookAt(GameObject.Find("Player").transform.position);
            }
            catch (NullReferenceException e)
            {
                Debug.Log("NullReferenceException");
            }
        }
    }

    void Accelerate()
    {
        //Acceleration
        if (Vector3.Dot(bossBody.velocity, transform.forward) <= max_speed)
        {
            bossBody.AddRelativeForce(Vector3.forward * speed_force);
        }
        //Deceleration
        if (Vector3.Dot(bossBody.velocity, transform.forward) > max_speed)
        {
            bossBody.AddRelativeForce(Vector3.forward * speed_force * -1);
        }
        if (Vector3.Dot(bossBody.velocity, -transform.right) > max_speed / 3.5f)
        {
            bossBody.AddRelativeForce(Vector3.right * speed_force * 2.3f);
        }
        if (Vector3.Dot(bossBody.velocity, transform.right) > max_speed / 3.5f)
        {
            bossBody.AddRelativeForce(Vector3.left * speed_force * 2.3f);
        }
    }
}
