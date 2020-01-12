using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BoidGroup : MonoBehaviour
{
    [SerializeField]
    private Material disappearMat;
    [SerializeField]
    private Speaker speaker;

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
        if (other.gameObject.layer != LayerMask.NameToLayer("Player"))
            return;

        Object.FindObjectOfType<OrcaState>().ChangeState(gameObject.tag, null);
                
        StartCoroutine("Disappear");

        SoundManager.Instance.PlayOneShot3DSe(ESeTable.Sparkle_2, speaker);
    }

    private IEnumerator Disappear()
    {
        GetComponent<Collider>().enabled = false;

        SkinnedMeshRenderer[] renderer = transform.GetComponentsInChildren<SkinnedMeshRenderer>();
        ParticleSystem[] particle = transform.GetComponentsInChildren<ParticleSystem>();
        foreach (var r in renderer)
            r.material = disappearMat;

        yield return new WaitForSeconds(0.5f);

        foreach (var r in renderer)
            r.transform.parent.parent.DOScale(0, 0.5f);

        foreach (var p in particle)
            p.Play();

        yield return new WaitForSeconds(1f);

        Destroy(gameObject);
    }
}
