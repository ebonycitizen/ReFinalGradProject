using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetComeTutorial : MonoBehaviour
{
    [SerializeField]
    private OrcaState orcaState;
    [SerializeField]
    private GameObject sendObj;
    [SerializeField]
    private Grab rightHand;
    [SerializeField]
    private Grab leftHand;
    [SerializeField]
    private PathMoveEvent moveEvent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(moveEvent.canTouch == true && (rightHand.GetIsApproach() || leftHand.GetIsApproach() ||Input.GetKeyDown(KeyCode.C)))
        {
            orcaState.ChangeState(gameObject.tag, sendObj);
            Destroy(gameObject);
        }
    }
}
