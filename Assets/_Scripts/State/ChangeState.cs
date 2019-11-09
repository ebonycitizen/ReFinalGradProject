using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeState : MonoBehaviour
{
    [SerializeField]
    private GameObject sendObj;
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
            orcaState = Object.FindObjectOfType<OrcaState>();
            bool hasChangeState = orcaState.ChangeState(gameObject.tag, sendObj);
            if (!hasChangeState)
                return;
            Destroy(gameObject);
        }
    }
}
