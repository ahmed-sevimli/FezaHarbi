using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour, Moveable
{
    private Rigidbody enemyBody; // Reference to player's Rigidbody.
    public Vector3 enemyDirection;
    private float speed_force = 40.0f;
    //private float recoil_force = -180.0f;
    private float timer;
    //private float max_speed = 20f;
    private float rot_speed = 360.0f;

    // Start is called before the first frame update
    void Start()
    {
        enemyBody = GetComponent<Rigidbody>(); // Access player's Rigidbody.
    }

    // Update is called once per frame
    void Update()
    {
        enemyDirection = Vector3.forward;
    }

    void FixedUpdate()
    {
        Navigate();
        //Accelerate();
    }

    public void Accelerate()
    {
        // if(Vector3.Dot(enemyBody.velocity, transform.forward) <= max_speed)
        // {
        //     enemyBody.AddRelativeForce(Vector3.forward * speed_force);
        // }
    }

    public void Navigate()
    {
        // Rotate enemy in player's direction.
        // float turnH = Input.GetAxis("Horizontal") * rot_speed * Time.fixedDeltaTime;
        // Quaternion turnRotation = Quaternion.Euler(0f, turnH, 0f);
        // enemyBody.MoveRotation(enemyBody.rotation * turnRotation);
    }

    public void StopMovement()
    {
        enemyBody.velocity = Vector3.zero;
    }

    public void Recoil()
    {
        // enemyBody.AddRelativeForce(Vector3.forward * recoil_force);
    }
}
