using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using UniRx.Triggers;
using UnityStandardAssets.ImageEffects;

public class StageManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] transferObj;

    private ChangeStage changeStage;
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
        changeStage = GameObject.FindObjectOfType<ChangeStage>();

        Scene oldScene = SceneManager.GetActiveScene();

        SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);

        foreach (GameObject obj in transferObj)
            SceneManager.MoveGameObjectToScene(obj, SceneManager.GetSceneByName(scene));

        yield return new WaitForSeconds(changeStage.GetWaitActiveSec());

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(scene));

        ChangeBGM(scene);
        ChangeFogData(changeStage);

        yield return new WaitForSeconds(changeStage.GetUnloadWaitSec());
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
                if (scene == "SeasideF")
                    SoundManager.Instance.DoFadeInBgm(EBgmTable.Seaside);
                if (scene == "OceanF")
                    SoundManager.Instance.DoFadeInBgm(EBgmTable.Ocean);
                if (scene == "CaveF")
                    SoundManager.Instance.DoFadeInBgm(EBgmTable.Ocean);
            });
    }

    private void ChangeFogData(ChangeStage changeStage)
    {
        GlobalFog globalFog = GameObject.FindObjectOfType<GlobalFog>();

        globalFog.distanceFog = changeStage.GetFogData().distanceFog;
        globalFog.useRadialDistance = changeStage.GetFogData().useRadialDistance;
        globalFog.heightFog = changeStage.GetFogData().heightFog;
        globalFog.height = changeStage.GetFogData().height;
        globalFog.heightDensity = changeStage.GetFogData().heightDensity;
        globalFog.startDistance = changeStage.GetFogData().startDistance;
    }
}
