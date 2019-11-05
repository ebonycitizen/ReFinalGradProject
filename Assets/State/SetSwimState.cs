using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSwimState : MonoBehaviour
{
    private OrcaState orcaState;
    // Start is called before the first frame update
    void Start()
    {
        orcaState = Object.FindObjectOfType<OrcaState>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Dolly"))
        {
            orcaState.GotoSwimState();
        }
    }
}