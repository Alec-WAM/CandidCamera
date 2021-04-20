using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;


// the training for the camera, simply guesses if there's something wrong in the scene

public class VisualCameraAgentBlock1 : Agent
{
    private bool isJaywalking;

    public PlayerDetectionHandler detectionHandler;

    public Text debugInfo;
    public int guesses;
    public int hits;
    public bool jWalkGuessing;
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
            string text = "Accuracy: " + hitRate + " " + "Guessing: " + jWalkGuessing;


            debugInfo.text = text;
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        
    }

    public void detectPlayer(bool detected)
    {
        if(detectionHandler != null)
        {
            detectionHandler.triggerDetection(detected);
        }   
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        var discreteActions = actions.DiscreteActions;

        jWalkGuessing = (int)discreteActions[0] > 0;
        Debug.Log("Guessing " + discreteActions[0]);

        
        if (jWalkGuessing)
        {
            guesses++;
            if (isJaywalking)
            {
                SetReward(1.0F);
                Debug.Log("Caught");
                hits++;
                detectPlayer(true); //Based on time of decisions 
                EndEpisode();
            }
            else
            {
                //AddReward(-0.001F);
                AddReward(-0.1F);
                detectPlayer(false); //Based on time of decisions 
                Debug.Log("Miss");
            }
            //EndEpisode();
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
