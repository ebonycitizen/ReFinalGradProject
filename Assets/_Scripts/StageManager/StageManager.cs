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
    private GlobalFog globalFog;
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
        
        float elaspedTime = 0f;
        while (elaspedTime < 1f)
        {
            RenderSettings.ambientSkyColor = Color.Lerp(RenderSettings.ambientSkyColor, changeStage.GetFogData().SkyColor, elaspedTime);
            RenderSettings.ambientEquatorColor = Color.Lerp(RenderSettings.ambientEquatorColor, changeStage.GetFogData().EquatorColor, elaspedTime);
            RenderSettings.ambientGroundColor = Color.Lerp(RenderSettings.ambientGroundColor, changeStage.GetFogData().GroundColor, elaspedTime);
            RenderSettings.fogColor = Color.Lerp(RenderSettings.fogColor, changeStage.GetFogData().fogColor, elaspedTime);
            elaspedTime += Time.deltaTime;
            yield return null;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(scene));

        ChangeBGM(scene);
        ChangeFogData();

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
        //var _duration = 1;
        //SoundManager.Instance.DoFadeOutBgm(duration: _duration);

        //Observable.Timer(TimeSpan.FromSeconds(_duration))
        //    .Subscribe(_ =>
        //    {
        SoundManager.Instance.StopAllBgm();

        if (scene == "TutorialF")
            SoundManager.Instance.PlayBgm(EBgmTable.Tutorial);
        if (scene == "IceF")
            SoundManager.Instance.PlayBgm(EBgmTable.Seaside);
        if (scene == "CoralF")
            SoundManager.Instance.PlayBgm(EBgmTable.Ocean);
        if (scene == "CaveF")
            SoundManager.Instance.PlayBgm(EBgmTable.Cave);
            //});
    }

    private void ChangeFogData()
    {
        GlobalFog globalFog = GameObject.FindObjectOfType<GlobalFog>();
        
        globalFog.distanceFog =  changeStage.GetFogData().distanceFog;
        globalFog.useRadialDistance = changeStage.GetFogData().useRadialDistance;
        globalFog.heightFog = changeStage.GetFogData().heightFog;
        globalFog.height = changeStage.GetFogData().height;
        globalFog.heightDensity = changeStage.GetFogData().heightDensity;
        globalFog.startDistance = changeStage.GetFogData().startDistance;
    }
}
