using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerJump : MonoBehaviour
{
    [SerializeField]
    private Dolphin[] dolphins;

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
            foreach (Dolphin d in dolphins)
                d.Jump();

            Invoke("PlayJump", 0.7f);
        }
    }
    private void PlayJump()
    {
        orcaState.GotoPlayerJumpState();
    }
}
