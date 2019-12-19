using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

public class LoadTutorial : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SteamVR_Fade.Start(new Color(0, 0, 0, 1), 0f);
        StartCoroutine("StartLoad");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator StartLoad()
    {
        Scene curScene = SceneManager.GetActiveScene();
        AsyncOperation async = SceneManager.LoadSceneAsync("TutorialF", LoadSceneMode.Additive);

        //async.allowSceneActivation = false;
        yield return new WaitForSeconds(3f);

        SceneManager.UnloadSceneAsync("Loading");
        //async.allowSceneActivation = true;
    }
}
