using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour, Moveable
{
    private Rigidbody playerBody; // Reference to player's Rigidbody.
    private float speed_force = 30.0f;
    private float recoil_force = -180.0f;
    private float dash_force = 2000f;
    private float max_speed = 25f;
    //private float bump_force = 0.5f;
    //private float recovery_time = 0.5f;
    private float rot_speed = 300.0f;
    private bool canDash = true;
    private float deltaDash = 0;
    private float dashWait = 1f;
    private float dashRate = 3f;
    private bool dashRight;
    private float prev_vel;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody>(); // Access player's Rigidbody.
    }

    // Update is called once per frame
    void Update()
    {
        if(canDash)
        {
            if(Input.GetKey(KeyCode.E))
            {
                //Dash right
                dashRight = true;
                Dash();
            }
            else if(Input.GetKey(KeyCode.Q))
            {
                //Dash left
                dashRight = false;
                Dash();
            }
            
        }
        else if(!canDash)
        {
            //deltaDash code
            if(deltaDash < dashWait)
            {
                deltaDash += Time.deltaTime;
                //dash animation code
                if(deltaDash <= dashWait / dashRate)
                {
                    if(dashRight)
                    {
                        transform.Rotate(0, 0, 360f * dashRate * Time.deltaTime, Space.Self);
                    }
                    else
                    {
                        transform.Rotate(0, 0, -360f * dashRate * Time.deltaTime, Space.Self);
                    }
                }
            }
            else
            {
                canDash = true;
                deltaDash = 0;
            }
        }
    }

    void FixedUpdate()
    {
        Navigate();
        Accelerate();
    }

    public void Accelerate()
    {
        //Acceleration
        if(Vector3.Dot(playerBody.velocity, transform.forward) <= max_speed)
        {
            playerBody.AddRelativeForce(Vector3.forward * speed_force);
        }
        //Deceleration
        if(Vector3.Dot(playerBody.velocity, transform.forward) > max_speed)
        {
            playerBody.AddRelativeForce(Vector3.forward * speed_force * -1);
        }
        if(Vector3.Dot(playerBody.velocity, -transform.right) > max_speed/3.5f)
        {
            playerBody.AddRelativeForce(Vector3.right * speed_force * 2.3f);
        }
        if(Vector3.Dot(playerBody.velocity, transform.right) > max_speed/3.5f)
        {
            playerBody.AddRelativeForce(Vector3.left * speed_force * 2.3f);
        }
    }

    public void Navigate()
    {
        // Rotate player based on horizontal input.
        float turnH = Input.GetAxis("Horizontal") * rot_speed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turnH, 0f);
        playerBody.MoveRotation(playerBody.rotation * turnRotation);
    }

    // public void oldDash(bool dashRight)
    // {
    //     canDash = false;
    //     deltaDash = 0;
    //     StopMovement();
    //     if(dashRight)
    //     {
    //         transform.Rotate(0, 90, 0, Space.Self);
    //         playerBody.AddRelativeForce(Vector3.right * 2000f);
    //     }
    //     else
    //     {
    //         transform.Rotate(0, -90, 0, Space.Self);
    //         playerBody.AddRelativeForce(Vector3.right * -2000f);
    //     }
    //     Debug.Log("Dash!");
    // }

    public void Dash()
    {
        canDash = false;
        if(dashRight)
        {
            playerBody.AddRelativeForce(Vector3.right * dash_force);
        }
        else
        {
            playerBody.AddRelativeForce(Vector3.left * dash_force);
        }
        Debug.Log("Dash!");
    }

    public void StopMovement()
    {
        playerBody.velocity = Vector3.zero;
    }

    public void Recoil()
    {
        playerBody.AddRelativeForce(Vector3.forward * recoil_force);
    }

    // public void RecoverFromBump()
    // {

    // }

    // IEnumerator Example()
    // {
    //     //yield return new WaitForFixedUpdate();
    //     // timer = 0.0f;
    //     // while (timer < acc_time)
    //     // {
    //     //     timer += Time.fixedDeltaTime;
    //     //     rb.AddForce(new Vector3(speed_force, 0f, 0f), ForceMode.Force);
    //     //     Example();
    //     // }
    // }
}
