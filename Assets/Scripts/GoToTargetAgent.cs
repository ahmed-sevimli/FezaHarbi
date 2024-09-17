using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class GoToTargetAgent : Agent
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Material winMaterial;
    [SerializeField] private Material loseMaterial;
    [SerializeField] private MeshRenderer floorMesh;

    private Rigidbody aiPlayerBody;
    private int wallHitCount = 0;
    [SerializeField] private int hitLimit;
    private float speed_force = 30.0f;
    private float max_speed = 25f;
    private float rot_speed = 300.0f;

    private void Start()
    {   
        // Access player's Rigidbody.
        aiPlayerBody = GetComponent<Rigidbody>();
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
        transform.localPosition = new Vector3(Random.Range(-36f, 36f), -9.284155f, Random.Range(-18f, 18f));
        targetTransform.localPosition = new Vector3(Random.Range(-37f, 37f), -9.284155f, Random.Range(-18.5f, 18.5f));
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(targetTransform.localPosition);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float turnH = (actions.ContinuousActions[0] * rot_speed / 2.3f) * Time.fixedDeltaTime;
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
        Debug.Log("Trigger");
        if (collided.TryGetComponent<Target>(out Target target))
        {
            Debug.Log("Here is Goal");
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
