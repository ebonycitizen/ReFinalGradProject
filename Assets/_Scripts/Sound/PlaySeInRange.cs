using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySeInRange : MonoBehaviour
{
    [SerializeField]
    private ESeTable se;
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
        if(other.gameObject.layer == LayerMask.NameToLayer("Dolly"))
        {
            SoundManager.Instance.PlayOneShot3DSe(se, speaker);
        }
    }
}
