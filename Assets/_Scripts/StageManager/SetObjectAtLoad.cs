using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetObjectAtLoad : MonoBehaviour
{
    [SerializeField]
    private GameObject[] objDisable;

    [SerializeField]
    private float unloadTime;

    //private void EnableObject()
    //{
    //    foreach (GameObject obj in objEnable)
    //        obj.SetActive(true);
    //}

    private void DisableObject()
    {
        foreach (GameObject obj in objDisable)
            obj.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
    {
        Debug.Log(prevScene.name + "->" + nextScene.name);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.activeSceneChanged -= OnActiveSceneChanged;
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
        Invoke("DisableObject", unloadTime);
        Debug.Log(scene.name + " scene loaded");
    }

    void OnSceneUnloaded(Scene scene)
    {
        Debug.Log(scene.name + " scene unloaded");
    }

}
