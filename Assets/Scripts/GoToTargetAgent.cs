using UnityEngine;
using Random = UnityEngine.Random;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using static UnityEngine.GraphicsBuffer;
using System;

public class GoToTargetAgent : Agent
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Transform centerTransform;
    [SerializeField] private Material winMaterial;
    [SerializeField] private Material loseMaterial;
    [SerializeField] private MeshRenderer floorMesh;

    private Rigidbody aiPlayerBody;
    private int wallHitCount = 0;
    [SerializeField] private int hitLimit;
    private float speed_force = 30.0f;
    private float max_speed = 25f;
    private float rot_speed = 300.0f;
    private float angleBetween = 0.0f;
    private Vector3 targetDir;

    private float prevDistance;
    private float prevBestDistance;
    private float currDistance;
    private int choiceOfRegion;
    private int secondChoiceOfRegion;

    private void Start()
    {   
        // Access player's Rigidbody.
        aiPlayerBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        currDistance = Vector3.Distance(transform.position, targetTransform.position);
        targetDir = targetTransform.position - transform.position;
        angleBetween = Vector3.Angle(transform.forward, targetDir);
        if(currDistance >= 1f)
        {
            RewardOnDistance();
        }
        if(angleBetween >= 1f)
        {
            RewardOnRotation();
        }
    }

    void RewardOnDistance()
    {
        

        //Dynamic Rewarding
        if (currDistance < prevDistance)
        {
            SetReward(1f / (currDistance + 1f));
        }
        else if (currDistance > prevDistance)
        {
            SetReward(-1f / (currDistance + 1f));
        }
        prevDistance = currDistance;

        //Progressive Rewarding
        currDistance = Vector3.Distance(transform.position, targetTransform.position);
        if (currDistance < prevBestDistance)
        {
            SetReward(1f / (currDistance + 1f));
            prevBestDistance = currDistance;
        }
    }

    void RewardOnRotation()
    {
        //Reward for Looking at Target
        if(Math.Abs(angleBetween) < 15f)
        {
            SetReward(1f * currDistance / (angleBetween * 100f));
        }
    }

    void FixedUpdate()
    {
        Accelerate();
    }

    public void Accelerate()
    {
        //Acceleration
        if (Vector3.Dot(aiPlayerBody.velocity, transform.forward) <= max_speed)
        {
            aiPlayerBody.AddRelativeForce(Vector3.forward * speed_force);
        }
        //Deceleration
        if (Vector3.Dot(aiPlayerBody.velocity, transform.forward) > max_speed)
        {
            aiPlayerBody.AddRelativeForce(Vector3.forward * speed_force * -1);
        }
        if (Vector3.Dot(aiPlayerBody.velocity, -transform.right) > max_speed / 3.5f)
        {
            aiPlayerBody.AddRelativeForce(Vector3.right * speed_force * 2.3f);
        }
        if (Vector3.Dot(aiPlayerBody.velocity, transform.right) > max_speed / 3.5f)
        {
            aiPlayerBody.AddRelativeForce(Vector3.left * speed_force * 2.3f);
        }
    }

    public override void OnEpisodeBegin()
    {
        //Instantiate previous distance
        prevBestDistance = prevDistance = Vector3.Distance(transform.position, targetTransform.position);

        //random spawn for spaceship and goal
        choiceOfRegion = Random.Range(0, 4);
        secondChoiceOfRegion = Random.Range(0, 4);
        while (choiceOfRegion == secondChoiceOfRegion)
        {
            secondChoiceOfRegion = Random.Range(0, 4);
        }
        //stop spaceship
        aiPlayerBody.velocity = Vector3.zero;
        //spawn both
        Spawner(transform, choiceOfRegion);
        Spawner(targetTransform, secondChoiceOfRegion);
        //central rotation
        transform.LookAt(centerTransform);
    }

    public void Spawner(Transform trns, int choice)
    {
        if (choice == 0)
        {
            trns.localPosition = new Vector3(Random.Range(0f, 36f), -9.284155f, Random.Range(0f, 18f));
        }
        else if (choice == 1)
        {
            trns.localPosition = new Vector3(Random.Range(0f, 36f), -9.284155f, Random.Range(-18f, 0f));
        }
        else if (choice == 2)
        {
            trns.localPosition = new Vector3(Random.Range(-36f, 0f), -9.284155f, Random.Range(-18f, 0f));
        }
        else if (choice == 3)
        {
            trns.localPosition = new Vector3(Random.Range(-36f, 0f), -9.284155f, Random.Range(0f, 18f));
        }
        else
        {
            Debug.Log("Error on spawn");
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(transform.rotation);
        sensor.AddObservation(targetTransform.localPosition);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float turnH = (actions.ContinuousActions[0] * rot_speed / 2f) * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turnH, 0f);
        aiPlayerBody.MoveRotation(aiPlayerBody.rotation * turnRotation);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
    }

    private void OnTriggerEnter(Collider collided)
    {
        //Debug.Log("Trigger");
        if (collided.TryGetComponent<Target>(out Target target))
        {
            //Debug.Log("Here is Goal");
            SetReward(1f);
            EndOfEpisode(winMaterial);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Wall>(out Wall wall))
        {
            wallHitCount++;
            SetReward(-1f / hitLimit);
            if (wallHitCount >= hitLimit)
            {
                EndOfEpisode(loseMaterial);
            }
        }
    }

    private void EndOfEpisode(Material floorMaterial)
    {
        floorMesh.material = floorMaterial;
        wallHitCount = 0;
        EndEpisode();
    }
}
