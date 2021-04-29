using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class WinTrigger : MonoBehaviour
{

    public GameObject BlackoutSquare;
    public Text winText;
    // Start is called before the first frame update
    void Start()
    {
        winText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
           StartCoroutine(WinScreen());
        }
    }

    private IEnumerator WinScreen()
    {
        // Start fadeout
        Color objColor = BlackoutSquare.GetComponent<Image>().color;
        float fadeAmount;
        float fadeSpeed = 0.3f;

        winText.enabled = true;

        while (BlackoutSquare.GetComponent<Image>().color.a < 1)
        {
            fadeAmount = objColor.a + (fadeSpeed * Time.deltaTime);

            objColor = new Color(objColor.r, objColor.g, objColor.b, fadeAmount);
            BlackoutSquare.GetComponent<Image>().color = objColor;
            yield return null;
        }

        var sceneHandler = GameObject.FindObjectsOfType<SceneHandler>()[0];
        Debug.Log("Destroy SceneHandler " + (sceneHandler != null));
        Destroy(sceneHandler.gameObject);

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
        winText.enabled = false;
    }
}
