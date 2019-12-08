using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Spark : MonoBehaviour
{
    [SerializeField]
    private GameObject core;

    [SerializeField]
    private ParticleSystem action;

    [SerializeField]
    private Transform firefly;
    [SerializeField]
    private Light light;

    [SerializeField]
    private Material scanMat;

    public Material GetScanMaterial()
    {
        return scanMat;
    }

    private void Awake()
    {
        light.intensity = 0f;
        firefly.localScale = Vector3.zero;
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
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            StartCoroutine("StartUp");
        }
    }

    private IEnumerator StartUp()
    {
        GetComponent<Collider>().enabled = false;
        action.Play();
        firefly.GetComponent<ParticleSystem>().Play();

        firefly.DOScale(1, 1f);

        light.DOIntensity(1, 2);

        yield return null;

        core.SetActive(false);
    }
}
