using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public GameObject Target;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnBeforeSceneLoadRuntimeMethod()
    {
        Debug.Log("Before scene loaded");
    }

    void Update()
    {
        if (Target == null || !Target.scene.IsValid())
            return;
        
        // The target's scene is loaded and active
        var script = Target.gameObject.GetComponent<HumanTestScript>();
        if (script == null || !script.AtAimPoint)
            return;
        
        // Target is at aim point in scene, transition to the next scene
        StartCoroutine(LoadSceneAsync(SceneManager.GetActiveScene().name, "StraightBlock"));
    }

    IEnumerator LoadSceneAsync(string fromName, string toName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(toName);
        // Wait until scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        
        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(fromName);
        
        while (!asyncUnload.isDone)
        {
            yield return null;
        }
    }
}
