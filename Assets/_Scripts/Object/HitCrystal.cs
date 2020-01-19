using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCrystal : MonoBehaviour
{
    [SerializeField]
    private GameObject hitEffect;
    [SerializeField]
    private GameObject crack;

    private Transform cameraEye;

    // Start is called before the first frame update
    void Start()
    {
        cameraEye = Object.FindObjectOfType<RayFromCamera>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Head"))
        {

            Instantiate(hitEffect,cameraEye);

            Instantiate(crack, cameraEye);
            SoundManager.Instance.PlayOneShotSe(ESeTable.HeadHitCrystal,1);
            GetComponent<Collider>().enabled = false;
        }
    }
}
