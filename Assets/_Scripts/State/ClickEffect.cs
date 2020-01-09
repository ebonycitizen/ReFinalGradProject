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
    private Speaker speaker;

    public bool hasDone { get; private set; }

    private Transform orca;

    void Start()
    {
        hasDone = false;
    }
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Dolly"))
        {
            Break();
        }
    }

    private void Break()
    {
        GetComponentInChildren<ParticleSystem>().Play();

        Rigidbody[] rigid = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody r in rigid)
            r.isKinematic = false;

        SoundManager.Instance.PlayOneShot3DSe(ESeTable.Crystal, speaker);
    }

    private IEnumerator Click()
    {
        Vector3 dir = (glitterPos.position - orca.position);
        Instantiate(clickEffect, orca.position + orca.forward * 20f, Quaternion.LookRotation(dir));

        yield return new WaitForSeconds(0.8f);

        Break();

        Destroy(this);
        hasDone = true;
        yield return null;
    }

    public void StartEffect(Transform orca)
    {
        this.orca = orca;

        StartCoroutine("Click");
    }
}
