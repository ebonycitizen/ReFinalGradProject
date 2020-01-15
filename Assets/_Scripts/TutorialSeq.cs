using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class TutorialSeq : MonoBehaviour
{
    [SerializeField]
    private GameObject sunShaft;
    [SerializeField]
    private GameObject firstGlitter;

    private float intensity;

    private void Awake()
    {
        
        var lights = sunShaft.GetComponentsInChildren<Light>();
        intensity = lights[0].intensity;
        foreach (var l in lights)
        {
            l.intensity = 0f;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SteamVR_Fade.Start(new Color(0, 0, 0, 1), 0f);
        SceneManager.activeSceneChanged += OnActiveScene;



#if DEBUG
        if (SceneManager.sceneCount == 1)
        {
            var duration = 5f;
            SteamVR_Fade.Start(new Color(0, 0, 0, 0), duration, true);
            EnableLight(duration * 10f);
            Invoke("EnableGameObject", 1f);
        }
#endif
    }

    private void EnableLight(float duration)
    {
        var lights = sunShaft.GetComponentsInChildren<Light>();

        foreach (var l in lights)
        {
            l.intensity = 0f;
            l.DOIntensity(intensity, duration);
        }
    }

    private void EnableGameObject()
    {
        //sunShaft.SetActive(true);
        firstGlitter.SetActive(true);
        SoundManager.Instance.PlayOneShotSe(ESeTable.Sparkle_1, 0.5f);
    }

    void OnActiveScene(Scene prevScene, Scene nextScene)
    {
        var duration = 5f;
        SteamVR_Fade.Start(new Color(0,0,0,0), duration);
        EnableLight(duration);
        Invoke("EnableGameObject", 1f);
        //EnableGameObject();

        SceneManager.activeSceneChanged -= OnActiveScene;
    }
}
