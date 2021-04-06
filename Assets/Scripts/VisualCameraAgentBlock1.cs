using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class VisualCameraAgentBlock1 : Agent
{
    private bool isJaywalking;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(isJaywalking);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        var discreteActions = actions.DiscreteActions;

        bool jWalkGuess = (int)discreteActions[0] > 0;

        if (jWalkGuess)
        {
            if (isJaywalking)
            {
                AddReward(1.0F);
                Debug.Log("Caught");
            }
            else
            {
                AddReward(-1.0F);
                Debug.Log("Miss");
            }
        }
    }

    public void triggerJaywalking()
    {
        //Debug.Log("J-Walking!");
        isJaywalking = true;
    }

    public void stopJaywalking()
    {
        isJaywalking = false;
    }
}
