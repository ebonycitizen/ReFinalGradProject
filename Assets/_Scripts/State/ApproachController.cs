using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApproachController : MonoBehaviour
{
    [SerializeField]
    private bool canApproach;

    private LockOn lockOn;

    // Start is called before the first frame update
    void Start()
    {
        lockOn = Object.FindObjectOfType<LockOn>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Dolly"))
        {
            lockOn = Object.FindObjectOfType<LockOn>();
            lockOn.CanTouch = canApproach;
            lockOn.CancelApproach();
            Destroy(gameObject);
        }
    }
}
