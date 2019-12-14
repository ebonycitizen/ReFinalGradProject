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
    private Material scanMat;

    private float mainIntensity;

    public Material GetScanMaterial()
    {
        return scanMat;
    }

    private void OnEnable()
    {
        mainLight.DOIntensity(mainIntensity, 0.4f);
    }

    private void Awake()
    {
        mainIntensity = mainLight.intensity;
        mainLight.intensity = 0f;

        light.intensity = 0f;
        light.transform.localScale = Vector3.zero;
        firefly.localScale = Vector3.zero;

        gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
       
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

        yield return null;

        core.SetActive(false);
    }
}
