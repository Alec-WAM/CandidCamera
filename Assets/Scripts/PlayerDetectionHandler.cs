using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerDetectionHandler : MonoBehaviour
{
    public float timer;
    public float maxTime = 10.0f;
    public bool detected = false;

    public Text counterText;
    public Slider timerSlider;
    public GameObject BlackoutSquare;
    public Text failText;
    // Start is called before the first frame update
    void Start()
    {
        failText.enabled = false;
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
        if(timerSlider != null)
        {
            timerSlider.value = (maxTime-timer) / maxTime;
        }
    }

    public void triggerDetection(bool value)
    {
        detected = value;
    }

    public void punishPlayer()
    {
        //Game Over Scene
        StartCoroutine(FailScreen());
    }

    private IEnumerator FailScreen()
    {
        // Start fadeout
        Color objColor = BlackoutSquare.GetComponent<Image>().color;
        float fadeAmount;
        float fadeSpeed = 0.3f;

        failText.enabled = true;

        while (BlackoutSquare.GetComponent<Image>().color.a < 1)
        {
            fadeAmount = objColor.a + (fadeSpeed * Time.deltaTime);

            objColor = new Color(objColor.r, objColor.g, objColor.b, fadeAmount);
            BlackoutSquare.GetComponent<Image>().color = objColor;
            yield return null;
        }

        var loadAsync = SceneManager.LoadSceneAsync("StraightBlock");
        while (!loadAsync.isDone)
        {
            yield return new WaitForSeconds(0.1f);
        }

        while (BlackoutSquare.GetComponent<Image>().color.a > 0)
        {
            fadeAmount = objColor.a - (fadeSpeed * Time.deltaTime);

            objColor = new Color(objColor.r, objColor.g, objColor.b, fadeAmount);
            BlackoutSquare.GetComponent<Image>().color = objColor;
            yield return null;
        }
        failText.enabled = false;
    }
}
