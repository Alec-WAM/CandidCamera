using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
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
        if (Vector3.Distance(player.transform.position, target.transform.position) > 0.4f)
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
        target = GameObject.FindWithTag("TargetPoint");
        
        DontDestroyOnLoad(gameObject);
    }

    private IEnumerator TransitionToNewScene(Action<bool> callback)
    {
        if (sceneIndex + 1 == SceneNames.Length)
        {
            // At end of
            Debug.LogWarning("No further scenes!");
            yield break;
        }

        var curName = SceneManager.GetActiveScene().name;
        var newName = SceneNames[sceneIndex + 1];
        
        var loadAsync = SceneManager.LoadSceneAsync(newName);
        while (!loadAsync.isDone)
        {
            Debug.Log($"Waiting for scene '{newName}' to replace '{curName}'");
            yield return new WaitForSeconds(0.1f);
        }

        // Increment after scene
        sceneIndex++;
        
        // Remove current instance of player
        Destroy(GameObject.FindWithTag("Player"));
        
        // Instantiate player
        player = Instantiate(PlayerPrefab);
        player.transform.position = GameObject.FindWithTag("SpawnPoint").transform.position;
        target = GameObject.FindWithTag("TargetPoint");
        
        // Done transitioning to new scene
        callback(false);
    }
}
