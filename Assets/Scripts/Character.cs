using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, Shooter, Destructible
{
    
    public delegate void CharDestroyer(string name);
    public static event CharDestroyer CharDestruction;
    public Rigidbody ownerBody; // Reference to player's Rigidbody.
    protected int health;
    protected float fire_period;
    protected float eTimeAfterShoot = 0f;
    protected bool can_fire = false;
    protected Vector3 spawning_point;
    protected GameObject characterShot;
    protected Material midHealth;
    protected Material lowHealth;
    protected Renderer parentRend;
    protected Renderer childRend1;
    protected Renderer childRend2;
    // Start is called before the first frame update

    void Awake()
    {
        ownerBody = GetComponent<Rigidbody>();
    }

    public void Start()
    {
        //Get Renderers
        parentRend = gameObject.GetComponent<Renderer>();
        childRend1 = gameObject.transform.GetChild(1).GetComponent<Renderer>();
        childRend2 = gameObject.transform.GetChild(2).GetComponent<Renderer>();
    }

    // Update is called once per frame
    protected void Update()
    {
        if(!can_fire)
        {
            if(eTimeAfterShoot < fire_period)
            {
                eTimeAfterShoot += Time.deltaTime;
            }
            else
            {
                can_fire = true;
                //Debug.Log("Now you can fire!");
            }
        }
    }

    public void Spawn()
    {
        transform.position = spawning_point;
    }

    public void Shoot(Moveable charController)
    {
        if(can_fire)
        {
            //SpawnBullet
            GameObject _characterShot = Instantiate(characterShot);
            _characterShot.transform.position = transform.position;
            _characterShot.transform.rotation = transform.rotation;
            
            //FireBullet
            _characterShot.GetComponent<Shot>().GetFired();
            eTimeAfterShoot = 0f;
            can_fire = false;

            //Recoil
            charController.Recoil();
        }
    }

    public void TakeHit(){
    
        health--;
        Debug.Log("A Char has Taken a Hit!");
        if(health == 2)
        {
            parentRend.material = midHealth;
            childRend1.material = midHealth;
            childRend2.material = midHealth;
        }
        else if(health == 1)
        {
            parentRend.material = lowHealth;
            childRend1.material = lowHealth;
            childRend2.material = lowHealth;
        }
        else if(health <= 0)
        {
            SelfDestruct();
        }


    }

    public void SelfDestruct()
    {
        if(CharDestruction != null)
        {
            CharDestruction(gameObject.name);
        }
        Destroy(gameObject);
    }
}
