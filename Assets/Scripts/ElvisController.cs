using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class ElvisController: Agent
{
    Rigidbody rigidBody;
    public float speed = 5;

    public Transform TargetTransform;

    private GameObject goal;

    [SerializeField]
    private GameObject[] guitars = new GameObject[9];

    [SerializeField] 
    private GameObject fanGirl;

    // Start is called before the first frame update
    void Start()
    {
    }

    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(0, 0, -3);

        // Randomizing guitars' positions
        RandomizeGuitars();

        //Placing fangirl to her base position
        float decisionValue = UnityEngine.Random.Range(0, 2);
       
        if (decisionValue >= 1)
        {
            fanGirl.transform.localPosition = new Vector3(6.71f, 0, 0);
        } else
        {
            fanGirl.transform.localPosition = new Vector3(-8.9f, 0, 0);
        }

    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // The position of the agent
        sensor.AddObservation(transform.localPosition.x);
        sensor.AddObservation(transform.localPosition.y);

        // The position of the target guitar
        sensor.AddObservation(TargetTransform.localPosition.x);
        sensor.AddObservation(TargetTransform.localPosition.y);

        // The distance between the agent and the guitar
        sensor.AddObservation(Vector3.Distance(TargetTransform.localPosition, transform.localPosition));
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        var actionTaken = actions.ContinuousActions;

        float actionSpeed = (actionTaken[0] + 1) / 2; // [0, +1]
        float actionSteering = actionTaken[1]; // [-1, +1]

        transform.Translate(actionSpeed * Vector3.forward * speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(new Vector3(0, actionSteering * 180, 0));

        AddReward(-0.01f);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> actions = actionsOut.ContinuousActions;

        actions[0] = -1;
        actions[1] = 0;

        if (Input.GetKey("w"))
            actions[0] = 1;

        if (Input.GetKey("d"))
            actions[1] = +0.5f;

        if (Input.GetKey("a"))
            actions[1] = -0.5f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Wall" || collision.collider.tag == "OtherEquipment" || collision.collider.tag == "FanGirl")
        {
            AddReward(-1);
            EndEpisode();
        }
        else if (collision.collider.tag == "TargetGuitar")
        {
            AddReward(1);
            EndEpisode();
        }
    }

    private void RandomizeGuitars() {
        float[] xPositions = {-3.79f, -2.63f, -1.421f, -0.319f, 1.01f, 2.14f, 3.24f, 4.34f, 5.4274f};
        xPositions = ShuffleArray<float>(xPositions);
        

        for (int i=0; i<xPositions.Length; i++)
        {
            guitars[i].transform.localPosition = new Vector3(xPositions[i], 2.25f, 4.77f);
        }
    }

    private T[] ShuffleArray<T>(T[] array)
    {
        System.Random r = new System.Random();
        for (int i = array.Length; i > 0; i--)
        {
            int j = r.Next(i);
            T k = array[j];
            array[j] = array[i - 1];
            array[i - 1] = k;
        }

        return array;
    }

}
