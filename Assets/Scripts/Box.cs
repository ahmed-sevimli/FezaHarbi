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
    //public delegate void BoxDestroyer();
    //public static event BoxDestroyer BoxDestruction;
    public Action<string, string> OnBoxDestroy;
    void Awake()
    {
        // boxBody = GetComponent<Rigidbody>();
        // midHealth = Resources.Load("ball_mat1", typeof(Material)) as Material;
        // lowHealth = Resources.Load("ball_mat3", typeof(Material)) as Material;
    }

    // Start is called before the first frame update
    void Start()
    {
        // boxBody.AddRelativeForce(new Vector3(1260f, 680f, 340f));
        // if(String.Equals(SceneManager.GetActiveScene().name,"Level1")
        // || String.Equals(SceneManager.GetActiveScene().name,"Level2"))
        // {
        //     GameManager.Instance.SubscribeBoxAction(this);
        // }
    }

    public void Init()
    {
        boxBody = GetComponent<Rigidbody>();
        midHealth = Resources.Load("ball_mat1", typeof(Material)) as Material;
        lowHealth = Resources.Load("ball_mat3", typeof(Material)) as Material;
        boxBody.AddRelativeForce(new Vector3(1260f, 680f, 340f));

        if(String.Equals(SceneManager.GetActiveScene().name,"Level1")
        || String.Equals(SceneManager.GetActiveScene().name,"Level2"))
        {
            GameManager.Instance.SubscribeBoxAction(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
        if(OnBoxDestroy != null)
        {
            OnBoxDestroy(gameObject.name, "Box");
        }
        Destroy(gameObject);
    }
}
