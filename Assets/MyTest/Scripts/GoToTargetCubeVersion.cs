using UnityEngine;
using Random = UnityEngine.Random;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using static UnityEngine.GraphicsBuffer;
using System;

public class GoToTargetCubeVersion : Agent
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Transform centerTransform;
    [SerializeField] private Material winMaterial;
    [SerializeField] private Material loseMaterial;
    [SerializeField] private MeshRenderer floorMesh;

    private Rigidbody aiPlayerBody;
    private int wallHitCount = 0;
    [SerializeField] private int hitLimit;
    private float speed_force = 10.0f;
    private float max_speed = 10f;
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
            transform.position += transform.forward * Time.deltaTime * speed_force;
        }
    }

    public override void OnEpisodeBegin()
    {
        //Instantiate previous distance
        prevBestDistance = prevDistance = Vector3.Distance(transform.position, targetTransform.position);

        //random spawn for spaceship and goal
        transform.localPosition = new Vector3(Random.Range(8.5f, -4f), 3f, Random.Range(-7.5f, 4.66f));
        targetTransform.localPosition = new(Random.Range(8.5f, -4f), 3.031968f, Random.Range(-7.5f, 4.66f));
        //central rotation
        transform.LookAt(centerTransform);
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
        if (collided.TryGetComponent<Goal>(out Goal goal))
        {
            //Debug.Log("Here is Goal");
            SetReward(1f);
            EndOfEpisode(winMaterial);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Wally>(out Wally wall))
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
