using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickEffect : MonoBehaviour
{
    [SerializeField]
    private GameObject clickEffect;
    [SerializeField]
    private Transform glitterPos;
    [SerializeField]
    private GameObject defaultCrystal;

    Transform orca;
    public bool hasDone { get; private set; }
    void Start()
    {
        hasDone = false;
    }
    void Update()
    {
        
    }


    private IEnumerator Click()
    {
        Vector3 dir = (glitterPos.position - orca.position);
        Instantiate(clickEffect, orca.position + orca.forward * 20f, Quaternion.LookRotation(dir));

        yield return new WaitForSeconds(0.3f);


        defaultCrystal.SetActive(false);

        Rigidbody[] rigid = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody r in rigid)
            r.isKinematic = false;

        //Destroy(gameObject);
        hasDone = true;
        yield return null;
    }

    public void StartEffect(Transform orca)
    {
        this.orca = orca;

        StartCoroutine("Click");  
    }
}
