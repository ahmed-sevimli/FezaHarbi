using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour, Projectile
{
    public Rigidbody shotBody; // Reference to player's Rigidbody.
    private float speed_force = 100.0f;
    public bool fired = false;
    public string owner_tag;

    void Awake()
    {
        
    }

    // Start is called before the first frame update
    protected void Start()
    {
        shotBody = GetComponent<Rigidbody>(); // Access shot's Rigidbody.        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {

    }

    public void GetFired()
    {
        shotBody.velocity = transform.forward * speed_force;
    }

    protected void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Wall"))
        {
            //Hit the wall case
            //Stay is used because the bullets hit the wall fast and sometimes they get past
            Destroy(gameObject);
        }
    }

    protected void OnTriggerEnter(Collider col)
    {
        //Checks if the collided object is the owner of the shot
        if(!col.gameObject.CompareTag(owner_tag))
        {
            if(!col.gameObject.CompareTag("Wall"))
            {

                HitObject(col.gameObject);
                Destroy(gameObject);
            }
        }
    }

    public void HitObject(GameObject collidedObj)
    {
        //This is only for chars, not inanimate objects.
        //It might cause poblems in the future.

        if(collidedObj.GetComponent<Destructible>() != null)
        {
            collidedObj.GetComponent<Destructible>().TakeHit();

        }
    }
}
