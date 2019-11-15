using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using UniRx.Triggers;
public class StageManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] transferObj;
    [SerializeField]
    private float UnloadWaitSec;
    [SerializeField]
    private float waitActiveSec;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.DoFadeInBgm(EBgmTable.Tutorial, duration: 3, volume: 1);
    }

    private IEnumerator Load(string scene)
    {
        Scene oldScene = SceneManager.GetActiveScene();

        SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);

        foreach (GameObject obj in transferObj)
            SceneManager.MoveGameObjectToScene(obj, SceneManager.GetSceneByName(scene));

        yield return new WaitForSeconds(waitActiveSec);

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(scene));

        ChangeBGM(scene);

        yield return new WaitForSeconds(UnloadWaitSec);
        SceneManager.UnloadSceneAsync(oldScene);
    }

    public void LoadNextScene(string scene)
    {
        StartCoroutine("Load", scene);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void ChangeBGM(string scene)
    {
        var _duration = 1;
        SoundManager.Instance.DoFadeOutBgm(duration: _duration);

        Observable.Timer(TimeSpan.FromSeconds(_duration))
            .Subscribe(_ =>
            {
                if (scene == "TutorialF")
                    SoundManager.Instance.DoFadeInBgm(EBgmTable.Tutorial);
                if (scene == "SeasideF" || scene == "SeasideGes")
                    SoundManager.Instance.DoFadeInBgm(EBgmTable.Seaside);
                if (scene == "OceanF" || scene == "OceanGes")
                    SoundManager.Instance.DoFadeInBgm(EBgmTable.Ocean);
            });
    }
}
