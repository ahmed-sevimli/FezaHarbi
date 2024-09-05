using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, Shooter, Destructible
{
    
    public delegate void CharDestroyer(string name, string type, GameObject character);
    public event CharDestroyer CharDestruction;
    public Rigidbody ownerBody; // Reference to player's Rigidbody.
    protected int health;
    protected float fire_period;
    protected float eTimeAfterShoot = 0f;
    protected bool can_fire = false;
    protected Vector3 spawning_point;
    protected GameObject characterShot;
    protected Material midHealth;
    protected Material lowHealth;
    // Start is called before the first frame update

    void Awake()
    {
        ownerBody = GetComponent<Rigidbody>();
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
        /*
        if(health == 2)
        {
            gameObject.GetComponent<Renderer>().material = midHealth;
        }
        else if(health == 1)
        {
            gameObject.GetComponent<Renderer>().material = lowHealth;
        }
        else*/ if(health <= 0)
        {
            SelfDestruct();
        }
    }

    public void SelfDestruct()
    {
        if(CharDestruction != null)
        {
            CharDestruction(gameObject.name, "Character", gameObject);
        }
        Destroy(gameObject);
    }
}
