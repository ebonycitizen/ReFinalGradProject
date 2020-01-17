using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Spark : MonoBehaviour
{
    [SerializeField]
    private GameObject core;
    [SerializeField]
    private Light mainLight;

    [SerializeField]
    private ParticleSystem action;

    [SerializeField]
    private Transform firefly;
    [SerializeField]
    private Light light;
    [SerializeField]
    private Transform soul;

    [SerializeField]
    private Material scanMat;

    [SerializeField]
    private GameObject area;
    [SerializeField]
    private float areaDisappearTime = 30f;
    [SerializeField]
    private SparkBgmPart bgm;

    public SparkBgmPart GetBgm()
    {
        return bgm;
    }

    private Material soulMat;
    private float mainIntensity;

    public Material GetScanMaterial()
    {
        return scanMat;
    }

    public SparkBgmPart GetSparkBgm()
    {
        return bgm;
    }

    private void OnEnable()
    {
        float duration = 2f;
        mainLight.DOIntensity(mainIntensity, duration / 2);
        core.transform.DOScale(Vector3.one, duration);
        soulMat.DOColor(new Color(soulMat.color.r, soulMat.color.g, soulMat.color.b, 1f), duration);
    }

    private void Awake()
    {
        mainIntensity = mainLight.intensity;
        mainLight.intensity = 0f;

        light.intensity = 0f;
        light.transform.localScale = Vector3.zero;
        firefly.localScale = Vector3.zero;

        core.transform.localScale = Vector3.zero;
        soulMat = soul.GetComponentInChildren<SkinnedMeshRenderer>().material;
        soulMat.color = new Color(soulMat.color.r, soulMat.color.g, soulMat.color.b, 0f);

        
    }

    // Start is called before the first frame update
    void Start()
    {
       gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        //if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        //{
        //    StartCoroutine("StartUp");
        //}
    }

    public void StartUp()
    {
        StartCoroutine("Action");
    }

    private IEnumerator Action()
    {
        GetComponent<Collider>().enabled = false;
        action.Play();
        firefly.GetComponent<ParticleSystem>().Play();

        firefly.DOScale(1, 1f);

        light.transform.DOScale(1, 1f);
        light.DOIntensity(1, 2);
        soulMat.DOColor(new Color(soulMat.color.r, soulMat.color.g, soulMat.color.b, 0f), 1f);
        yield return null;

        core.SetActive(false);

        yield return new WaitForSeconds(areaDisappearTime);

        area.SetActive(false);
    }
}
