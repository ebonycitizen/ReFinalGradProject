using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowWaterSplash : MonoBehaviour
{
    [SerializeField]
    private GameObject splashPrefab;

    private RainCameraController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<RainCameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator StartEffect()
    {
        controller.Play();
        Instantiate(splashPrefab);
        yield return new WaitForSeconds(2f);
        controller.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Dolly"))
        {
            StartCoroutine("StartEffect");
        }
    }
}
