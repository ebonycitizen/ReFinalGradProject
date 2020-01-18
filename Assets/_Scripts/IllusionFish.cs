using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IllusionFish : MonoBehaviour
{
    [SerializeField]
    private Material disappearMat;
    [SerializeField]
    private Speaker speaker;
    [SerializeField]
    private ParticleSystem m_disapearEff;

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

        SoundManager.Instance.PlayOneShot3DSe(ESeTable.Fish, speaker);
    }

    private IEnumerator Disappear()
    {
        GetComponent<Collider>().enabled = false;

        var particleRenderer = GetComponent<ParticleSystemRenderer>();
        var particle = GetComponent<ParticleSystem>();

        particleRenderer.material = disappearMat;
        particle.Clear();

        m_disapearEff.Play();

        yield return new WaitForSeconds(1f);

        Destroy(gameObject);
    }
}
