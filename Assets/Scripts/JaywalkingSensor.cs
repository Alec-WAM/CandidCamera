using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JaywalkingSensor : MonoBehaviour
{
    public VisualCameraAgentBlock1 block1Agent;
    public Color GizmosColor = new Color(0.5f, 0.5f, 0.5f, 0.2f);

    void OnDrawGizmos()
    {
        Gizmos.color = GizmosColor;
        Gizmos.DrawCube(transform.position, transform.localScale);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionStay(Collision collisionInfo)
    {
        if(collisionInfo.gameObject.tag == "Player")
        {
            if(block1Agent != null)
            {
                block1Agent.triggerJaywalking();
            }
        }
    }
}
