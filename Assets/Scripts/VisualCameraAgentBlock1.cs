using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class VisualCameraAgentBlock1 : Agent
{
    private bool isJaywalking;


    public Text debugInfo;
    public int guesses;
    public int hits;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (guesses > 0)
        {
            float hitRate = (float)hits / (float)guesses;
            string text = "Accuracy: " + hitRate;
            debugInfo.text = text;
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        var discreteActions = actions.DiscreteActions;

        bool jWalkGuess = (int)discreteActions[0] > 0;
        Debug.Log("Guessing " + discreteActions[0]);

        if (jWalkGuess)
        {
            guesses++;
            if (isJaywalking)
            {
                AddReward(1.0F);
                Debug.Log("Caught");
                hits++;
                EndEpisode();
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
        isJaywalking = true;
    }

    public void stopJaywalking()
    {
        isJaywalking = false;
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsOut = actionsOut.DiscreteActions;
        discreteActionsOut[0] = Input.GetKey(KeyCode.Space) ? 1 : 0;
    }
}
