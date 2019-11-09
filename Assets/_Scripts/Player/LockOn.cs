using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using HI5;

public class LockOn : MonoBehaviour
{
    public SteamVR_Action_Boolean GrabAction;

    [SerializeField]
    private RayFromCamera rayCamera;
    [SerializeField]
    private Grab rightHand;
    [SerializeField]
    private Grab leftHand;

    [SerializeField]
    private float atkSpeedRequire;
    [SerializeField]
    private LayerMask targetLayer;
    [SerializeField]
    private OrcaState orcaState;

    GameObject cameraTarget;
    GameObject lockTarget;
    // Start is called before the first frame update
    void Start()
    {
        lockTarget = null;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        cameraTarget = rayCamera.LockOn(targetLayer);


        if (cameraTarget != null && cameraTarget != lockTarget)
            cameraTarget.GetComponent<Glitter>().HitEffect();


        if (cameraTarget == null && lockTarget != null)
            lockTarget.GetComponent<Glitter>().DisableEffect();

        //if (cameraTarget != null && (rightHand.GetVelocity().magnitude >= atkSpeedRequire ||
        //    leftHand.GetVelocity().magnitude >= atkSpeedRequire || GrabAction.stateDown))
        //{
        //    cameraTarget.GetComponent<Glitter>().StartUp(orcaState, cameraTarget);
        //}

        if (cameraTarget != null && cameraTarget.tag != "G_Come" && (rightHand.GetIsPoint() || leftHand.GetIsPoint()))
            cameraTarget.GetComponent<Glitter>().StartUp(orcaState, cameraTarget);

        if (cameraTarget != null && cameraTarget.tag == "G_Come" && (rightHand.GetIsApproach() || leftHand.GetIsApproach()))
            cameraTarget.GetComponent<Glitter>().StartUp(orcaState, cameraTarget);

        lockTarget = cameraTarget;
    }

}
