using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallRock : MonoBehaviour
{
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
        if (other.gameObject.layer == LayerMask.NameToLayer("Dolly"))
        {
            StartCoroutine("PlaySe");
            transform.parent.GetComponentInChildren<ParticleSystem>().Play();
            Rigidbody[] rigid = transform.parent.GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody r in rigid)
                r.isKinematic = false;
        }
    }

    private IEnumerator PlaySe()
    {
        SoundManager.Instance.PlayOneShot3DSe(ESeTable.Rock_1, speaker);
        yield return new WaitForSeconds(3);
        SoundManager.Instance.PlayOneShot3DSe(ESeTable.Rock_2, speaker);
        yield return null;
    }
}
