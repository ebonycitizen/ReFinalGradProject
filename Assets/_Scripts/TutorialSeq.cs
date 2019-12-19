using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.SceneManagement;

public class TutorialSeq : MonoBehaviour
{
    [SerializeField]
    private GameObject sunShaft;
    [SerializeField]
    private GameObject firstGlitter;

    private void Awake()
    {
        SteamVR_Fade.Start(new Color(0, 0, 0, 1), 0f);
        sunShaft.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        //#if DEBUG

        //#endif

            SceneManager.activeSceneChanged += OnActiveScene;
        //Invoke("StartSeq", 3);
    }

    private void StartSeq()
    {
        var duration = 5f;
        SteamVR_Fade.Start(new Color(0, 0, 0, 0), duration);
        sunShaft.SetActive(true);
        Invoke("EnableGameObject", 2f);
    }

    private void EnableGameObject()
    {
        
        firstGlitter.SetActive(true);
        SoundManager.Instance.PlayOneShotSe(ESeTable.Twinkle, 0.5f);
    }

    void OnActiveScene(Scene prevScene, Scene nextScene)
    {
        var duration = 5f;
        SteamVR_Fade.Start(new Color(0,0,0,0), duration);
        sunShaft.SetActive(true);
        Invoke("EnableGameObject", 2f);
        //EnableGameObject();

        SceneManager.activeSceneChanged -= OnActiveScene;
    }
}
