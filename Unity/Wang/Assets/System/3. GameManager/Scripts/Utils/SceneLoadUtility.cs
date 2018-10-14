using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadUtility : MonoBehaviour
{
    public string sceneToLoad = "Main";

    void Start()
    {
        StartCoroutine(Load(sceneToLoad));
    }

    IEnumerator Load(string sceneName)
    {
        yield return new WaitForEndOfFrame();
        GameManager.Instance.LoadLevel(sceneToLoad);
    }
}
