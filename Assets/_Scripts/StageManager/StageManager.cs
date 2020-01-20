using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using UniRx.Triggers;
using UnityStandardAssets.ImageEffects;
using UnityEngine.AI;

public class StageManager : SingletonMonoBehaviour<StageManager>
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
        SoundManager.Instance.DoFadeInBgm(EBgmTable.Tutorial, duration: 30, maxVolume: 0.8f);
    }

    private IEnumerator Load(string scene)
    {
        changeStage = GameObject.FindObjectOfType<ChangeStage>();
        globalFog = GameObject.FindObjectOfType<GlobalFog>();

        Scene oldScene = SceneManager.GetActiveScene();

        SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);

        foreach (GameObject obj in transferObj)
            SceneManager.MoveGameObjectToScene(obj, SceneManager.GetSceneByName(scene));

        yield return new WaitForSeconds(changeStage.GetWaitActiveSec());

        RenderSettings.skybox = changeStage.GetFogData().skybox;
        ChangeFogUsage();
        float elaspedTime = 0f;
        while (elaspedTime < 1f)
        {
            RenderSettings.ambientSkyColor = Color.Lerp(RenderSettings.ambientSkyColor, changeStage.GetFogData().SkyColor, elaspedTime);
            RenderSettings.ambientEquatorColor = Color.Lerp(RenderSettings.ambientEquatorColor, changeStage.GetFogData().EquatorColor, elaspedTime);
            RenderSettings.ambientGroundColor = Color.Lerp(RenderSettings.ambientGroundColor, changeStage.GetFogData().GroundColor, elaspedTime);
            RenderSettings.fogColor = Color.Lerp(RenderSettings.fogColor, changeStage.GetFogData().fogColor, elaspedTime);
            ChangeFogData(elaspedTime);

            elaspedTime += Time.deltaTime;
            yield return null;
        }


        SceneManager.SetActiveScene(SceneManager.GetSceneByName(scene));

        GameObject behaviour = GameObject.Find("Behavior Manager");
        if (behaviour != null)
            SceneManager.MoveGameObjectToScene(behaviour, SceneManager.GetSceneByName(scene));

        ChangeBGM(scene);

        yield return new WaitForSeconds(changeStage.GetUnloadWaitSec());
        SceneManager.UnloadSceneAsync(oldScene);
        ChangePostEffect();
    }

    public void LoadNextScene(string scene)
    {
        StartCoroutine("Load", scene);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void ChangePostEffect()
    {
        if (changeStage.GetPostEffect() != null)
            Instantiate(changeStage.GetPostEffect());
    }

    private void ChangeBGM(string scene)
    {
        //SoundManager.Instance.StopAllBgm();

        if (scene == "TutorialF")
            SoundManager.Instance.PlayBgm(EBgmTable.Tutorial);
        if (scene == "IceF")
        {
            //FadeOut
            SoundManager.Instance.PlayBgmWithoutLoop(EBgmTable.FullBgm, 0.8f);
            SoundManager.Instance.SetStartingBgmFlag(true);
        }
        if (scene == "BoneF")
        {
            //FadeIn
            //SoundManager.Instance.DoFadeOutBgm(duration: 1);
            //Observable.Timer(TimeSpan.FromSeconds(1))
            //    .Subscribe(_ =>
            //    {
            //        //SoundManager.Instance.DoFadeInBgmWithoutLoop(EBgmTable.LastBgm, 1, 0.8f);
                    
            //    }).AddTo(this);
            SoundManager.Instance.PlayBgmWithoutLoop(EBgmTable.LastBgm, 0.8f);
            SoundManager.Instance.SetLastBgmFlag(true);
        }
        if (scene == "CoralF")
            SoundManager.Instance.SetLastBgmFlag(false);
        //if (scene == "CaveF")
        //    SoundManager.Instance.PlayBgm(EBgmTable.Cave);
    }

    private void ChangeFogUsage()
    {
        globalFog.distanceFog = changeStage.GetFogData().distanceFog;
        globalFog.useRadialDistance = changeStage.GetFogData().useRadialDistance;
        globalFog.heightFog = changeStage.GetFogData().heightFog;
    }

    private void ChangeFogData(float elaspedTime)
    {
        globalFog.height = Mathf.Lerp(globalFog.height, changeStage.GetFogData().height, elaspedTime);
        globalFog.heightDensity = Mathf.Lerp(globalFog.heightDensity, changeStage.GetFogData().heightDensity, elaspedTime);
        globalFog.startDistance = Mathf.Lerp(globalFog.startDistance, changeStage.GetFogData().startDistance, elaspedTime);
    }
}
