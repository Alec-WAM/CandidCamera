using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneHandler : MonoBehaviour
{
    public GameObject BlackoutSquare;
    public GameObject PlayerPrefab;
    public string[] SceneNames;
    private GameObject player;
    private GameObject target;
    private int sceneIndex;
    private bool transitioning;

    public SceneHandler(string[] sceneNames)
    {
        SceneNames = sceneNames;
    }

    private void Update()
    {
        if (target == null || player == null || !target.scene.IsValid() ||!player.scene.IsValid())
            return;

        // The target's scene is loaded and active
        if (Vector3.Distance(player.transform.position, target.transform.position) > 0.5f)
            return;
        
        
    }

    public void nextScene()
    {
        if (player == null || !player.scene.IsValid())
            return;
        if (transitioning) return;

        // Target is at aim point in scene, transition to the next scene
        transitioning = true;
        StartCoroutine(TransitionToNewScene(trans => transitioning = trans));
    }

    private void Awake()
    {
        // Instantiate player
        player = Instantiate(PlayerPrefab);
        player.transform.position = GameObject.FindWithTag("SpawnPoint").transform.position;
        //target = GameObject.FindWithTag("TargetPoint");
        
        DontDestroyOnLoad(gameObject);
    }

    private IEnumerator TransitionToNewScene(Action<bool> callback)
    {
        // Start fadeout
        Color objColor = BlackoutSquare.GetComponent<Image>().color;
        float fadeAmount;
        float fadeSpeed = 5.0f;
        
        while (BlackoutSquare.GetComponent<Image>().color.a < 1)
        {
            fadeAmount = objColor.a + (fadeSpeed * Time.deltaTime);

            objColor = new Color(objColor.r, objColor.g, objColor.b, fadeAmount);
            BlackoutSquare.GetComponent<Image>().color = objColor;
            yield return null;
        }

        if (sceneIndex + 1 == SceneNames.Length)
        {
            // At end of
            Debug.LogWarning("No further scenes!");
            yield break;
        }

        var curName = SceneManager.GetActiveScene().name;
        var newName = SceneNames[sceneIndex + 1];

        // Destroy original player & spawn/target points
        Destroy(GameObject.FindWithTag("Player"));
        Destroy(GameObject.FindWithTag("SpawnPoint"));
        //Destroy(GameObject.FindWithTag("TargetPoint"));

        var loadAsync = SceneManager.LoadSceneAsync(newName);
        while (!loadAsync.isDone)
        {
            Debug.Log($"Waiting for scene '{newName}' to replace '{curName}'");
            yield return new WaitForSeconds(0.1f);
        }
        Debug.Log($"Scene '{newName}' replaced '{SceneManager.GetActiveScene().name}'");
        // Increment after scene
        sceneIndex++;

        // Retrieve BlackOutSquare for new scene
        BlackoutSquare = GameObject.FindWithTag("BlackOut");

        // Instantiate player
        player = Instantiate(PlayerPrefab);
        player.transform.position = GameObject.FindWithTag("SpawnPoint").transform.position;
        //target = GameObject.FindWithTag("TargetPoint");
        
        while (BlackoutSquare.GetComponent<Image>().color.a > 0)
        {
            fadeAmount = objColor.a - (fadeSpeed * Time.deltaTime);

            objColor = new Color(objColor.r, objColor.g, objColor.b, fadeAmount);
            BlackoutSquare.GetComponent<Image>().color = objColor;
            yield return null;
        }
        
        // Done transitioning to new scene
        callback(false);
    }
}
