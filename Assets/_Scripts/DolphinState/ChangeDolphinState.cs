using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDolphinState : MonoBehaviour
{
    [SerializeField]
    private DolphinState dolphinState;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Dolly"))
        {
            bool hasChangeState = dolphinState.ChangeState(gameObject.tag, null);
            if (!hasChangeState)
                return;
            Destroy(gameObject);
        }
    }
}
