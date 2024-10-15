using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiPlayerMovementController : MonoBehaviour, Moveable
{
    private Rigidbody playerBody; // Reference to player's Rigidbody.
    private float speed_force = 30.0f;
    private float recoil_force = -180.0f;
    private float dash_force = 2000f;
    private float max_speed = 25f;
    //private float bump_force = 0.5f;
    //private float recovery_time = 0.5f;
    private float rot_speed = 250.0f;
    private bool canDash = true;
    private float deltaDash = 0;
    private float dashWait = 1f;
    private float dashRate = 3f;
    private bool dashRight;
    private float prev_vel;

    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode leftDodgeKey;
    public KeyCode rightDodgeKey;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody>(); // Access player's Rigidbody.
    }

    // Update is called once per frame
    void Update()
    {
        Navigate();

        if(canDash)
        {
            if(Input.GetKey(rightDodgeKey))
            {
                //Dash right
                dashRight = true;
                Dash();
            }
            else if(Input.GetKey(leftDodgeKey))
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
        Accelerate();
    }

    public void Navigate()
    {
        if (Input.GetKey(rightKey))
        {
            transform.Rotate(Vector3.up, rot_speed * Time.deltaTime);
        }
        else if (Input.GetKey(leftKey))
        {
            transform.Rotate(Vector3.up, rot_speed * Time.deltaTime * -1);
        }
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
