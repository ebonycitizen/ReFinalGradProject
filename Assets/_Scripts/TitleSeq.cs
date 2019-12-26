using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using DG.Tweening;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class TitleSeq : MonoBehaviour
{
    [SerializeField]
    private RainCameraController rain;
    [SerializeField]
    private ParticleSystem drownEffect;
    [SerializeField]
    private float fadeDuration = 6f;
    [SerializeField]
    private Rigidbody player;
    [SerializeField]
    private Rigidbody floatObj;
    [SerializeField]
    private PostProcessVolume postProcess;
    [SerializeField]
    private GameObject sunShaft;

    [SerializeField]
    private Color topColor;
    [SerializeField]
    private Color medColor;
    [SerializeField]
    private Color downColor;

    private DepthOfField dof;
    private Material skyboxMat;
    private Color color1Ori;
    private Color color2Ori;
    private Color color3Ori;

    // Start is called before the first frame update
    void Start()
    {
        skyboxMat = RenderSettings.skybox;
        color1Ori = skyboxMat.GetColor("_Color1");
        color2Ori = skyboxMat.GetColor("_Color2");
        color3Ori = skyboxMat.GetColor("_Color3");

        floatObj.gameObject.SetActive(false);

        foreach (PostProcessEffectSettings item in postProcess.profile.settings)
        {
            if (item as DepthOfField)
            {
                dof = item as DepthOfField;
            };
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            StartCoroutine("StartUp");
    }

    private IEnumerator StartUp()
    {
        //SoundManager.Instance.PlayLoopSe(ESeTable.Water_4);

        yield return new WaitForSeconds(1f);

        rain.Play();

        yield return new WaitForSeconds(1f);

        drownEffect.Play();

        yield return new WaitForSeconds(2f);

        floatObj.gameObject.SetActive(true);

        player.AddForce(-Vector3.up * 2f, ForceMode.VelocityChange);
        floatObj.AddForce(Vector3.up * 5f, ForceMode.VelocityChange);


        var elaspedTime = dof.focusDistance.value;

        skyboxMat.DOColor(topColor, "_Color1", elaspedTime);
        skyboxMat.DOColor(medColor, "_Color2", elaspedTime);
        skyboxMat.DOColor(downColor, "_Color3", elaspedTime);

        while (elaspedTime >= 0)
        {
            elaspedTime -= Time.deltaTime;
            if (dof)
                dof.focusDistance.value = elaspedTime;

            yield return null;
        }
        //yield return new WaitForSeconds(5f);

        sunShaft.SetActive(false);
        SteamVR_Fade.Start(Color.black, fadeDuration);
        SoundManager.Instance.FadeAllSe(1f);
        rain.Stop();

        yield return new WaitForSeconds(5f);

        SceneManager.LoadSceneAsync("Loading", LoadSceneMode.Single);
    }

    private void OnDisable()
    {
        skyboxMat.SetColor("_Color1", color1Ori);
        skyboxMat.SetColor("_Color2", color2Ori);
        skyboxMat.SetColor("_Color3", color3Ori);
    }
}
