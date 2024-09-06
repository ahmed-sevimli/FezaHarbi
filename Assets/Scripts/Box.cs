using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;
public class Box : MonoBehaviour, Destructible
{
    private Rigidbody boxBody;
    private int health = 3;
    private Material midHealth;
    private Material lowHealth;

    public void Init()
    {
        boxBody = GetComponent<Rigidbody>();
        midHealth = Resources.Load("ball_mat1", typeof(Material)) as Material;
        lowHealth = Resources.Load("ball_mat3", typeof(Material)) as Material;
        boxBody.AddRelativeForce(new Vector3(1260f, 680f, 340f));
    }

    public void TakeHit()
    {
        Debug.Log("A Box has Taken a Hit!");
        health--;
        if(health == 2)
        {
            gameObject.GetComponent<Renderer>().material = midHealth;
        }
        else if(health == 1)
        {
            gameObject.GetComponent<Renderer>().material = lowHealth;
        }
        else if(health <= 0)
        {
            SelfDestruct();
        }
    }

    public void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
