using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetComeState : MonoBehaviour
{
    [SerializeField]
    private Grab rightHand;
    [SerializeField]
    private LayerMask targetLayer;
    [SerializeField]
    private OrcaState orcaState;
    [SerializeField]
    private GameObject glitter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Come();
    }

    private void Come()
    {
        if (rightHand.GetIsApproach())
        {
            glitter.GetComponent<Glitter>().StartUp(orcaState, glitter, null);
            Destroy(gameObject);
        }
    }
}
