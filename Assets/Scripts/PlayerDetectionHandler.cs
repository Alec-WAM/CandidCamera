using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDetectionHandler : MonoBehaviour
{
    public float timer;
    public float maxTime = 10.0f;
    public bool detected = false;

    public Text counterText;
    // Start is called before the first frame update
    void Start()
    {
        timer = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (detected)
        {
            if(timer > 0)
            {
                timer -= Time.deltaTime;
                if(timer <= 0.0F)
                {
                    punishPlayer();
                }
            } 
        }
        else
        {
            if (timer < maxTime)
            {
                timer += Time.deltaTime;
            }
        }
        if (counterText != null) counterText.text = "Counter: " + timer;
    }

    public void triggerDetection(bool value)
    {
        detected = value;
    }

    public void punishPlayer()
    {
        //Game Over Scene
    }
}
