using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallRock : MonoBehaviour
{
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
            transform.parent.GetComponentInChildren<ParticleSystem>().Play();
            Rigidbody[] rigid = transform.parent.GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody r in rigid)
                r.isKinematic = false;
        }
    }
}
