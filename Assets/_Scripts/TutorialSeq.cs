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
        
        
    }

    // Start is called before the first frame update
    void Start()
    {
        sunShaft.SetActive(false);
        SteamVR_Fade.Start(new Color(0, 0, 0, 1), 0f);
        SceneManager.activeSceneChanged += OnActiveScene;

#if DEBUG
        var duration = 5f;
        SteamVR_Fade.Start(new Color(0, 0, 0, 0), duration, true);
        //sunShaft.SetActive(true);
        Invoke("EnableGameObject", 2f);
#endif
    }

    private void EnableGameObject()
    {
        //sunShaft.SetActive(true);
        firstGlitter.SetActive(true);
        SoundManager.Instance.PlayOneShotSe(ESeTable.Twinkle, 0.5f);
    }

    void OnActiveScene(Scene prevScene, Scene nextScene)
    {
        var duration = 5f;
        SteamVR_Fade.Start(new Color(0,0,0,0), duration);
        Invoke("EnableGameObject", 2f);
        //EnableGameObject();

        SceneManager.activeSceneChanged -= OnActiveScene;
    }
}
