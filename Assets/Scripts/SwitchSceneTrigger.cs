using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSceneTrigger : MonoBehaviour
{

    public SceneHandler sceneHandler;
    // Start is called before the first frame update
    void Start()
    {
        sceneHandler = GameObject.FindObjectsOfType<SceneHandler>()[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger" + sceneHandler);
        if (other.gameObject.tag == "Player")
        {
            if(sceneHandler != null)
            {
                sceneHandler.nextScene();
            }
        }
    }
}
