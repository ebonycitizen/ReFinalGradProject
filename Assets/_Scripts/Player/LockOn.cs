using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using HI5;

public class LockOn : MonoBehaviour
{
    public SteamVR_Action_Boolean GrabAction;

    public bool CanTouch { get; set; }

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
    private GameObject orca;

    private GameObject cameraTarget;
    private GameObject rightTarget;
    private GameObject leftTarget;

    private GameObject lockTarget;
    private OrcaState orcaState;
    private OrcaCollision orcaCollision;

    // Start is called before the first frame update
    void Start()
    {
        lockTarget = null;
        CanTouch = false;
        orcaState = orca.GetComponent<OrcaState>();
        orcaCollision = orca.GetComponentInChildren<OrcaCollision>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        cameraTarget = rayCamera.LockOn(targetLayer);
        rightTarget = rightHand.LockOn(targetLayer);
        leftTarget = leftHand.LockOn(targetLayer);

        //if (cameraTarget != null && cameraTarget != lockTarget)
        //    cameraTarget.GetComponent<Glitter>().HitEffect();

        //if (cameraTarget == null && lockTarget != null)
        //    lockTarget.GetComponent<Glitter>().DisableEffect();

        //if (cameraTarget != null && (rightHand.GetVelocity().magnitude >= atkSpeedRequire ||
        //    leftHand.GetVelocity().magnitude >= atkSpeedRequire || GrabAction.stateDown))
        //{
        //    cameraTarget.GetComponent<Glitter>().StartUp(orcaState, cameraTarget);
        //}

        Point();
        Touch();
    
        //if (cameraTarget != null && cameraTarget.tag != "G_Appraoch" && cameraTarget.tag != "G_Come" && (rightHand.GetIsPoint() || leftHand.GetIsPoint()))
        //    cameraTarget.GetComponent<Glitter>().StartUp(orcaState, cameraTarget, null);

        //if (cameraTarget != null && cameraTarget.tag == "G_Come" && (rightHand.GetIsApproach() || leftHand.GetIsApproach()))
        //    cameraTarget.GetComponent<Glitter>().StartUp(orcaState, cameraTarget, null);

        //if (cameraTarget != null && cameraTarget.tag == "G_Approach")
        //{
        //    if (rightHand.GetIsApproach())
        //        cameraTarget.GetComponent<Glitter>().StartUp(orcaState, cameraTarget, rightHand.GetApproachObj());
        //    else if (leftHand.GetIsApproach())
        //        cameraTarget.GetComponent<Glitter>().StartUp(orcaState, cameraTarget, leftHand.GetApproachObj());
        //}

        lockTarget = cameraTarget;
    }

    public void CancelApproach()
    {
        if(!CanTouch)
            orcaState.ChangeState("G_Idle", null);
    }

    private void Point()
    {
        if (rightTarget != null && rightTarget.tag != "G_Appraoch" && rightTarget.tag != "G_Come"
           && rightHand.GetIsPoint())
        {
            rightTarget.GetComponent<Glitter>().StartUp(orcaState, rightTarget, null);
        }

        if (leftTarget != null && leftTarget.tag != "G_Appraoch" && leftTarget.tag != "G_Come"
            && leftHand.GetIsPoint())
        {
            leftTarget.GetComponent<Glitter>().StartUp(orcaState, leftTarget, null);
        }
    }

    private void Touch()
    {
        if (rightHand.GetIsApproach())
        {
            if (CanTouch)
                orcaState.ChangeState("G_Approach", rightHand.GetApproachObj());
            else
                orcaCollision.PlayNoEffect();
           // approachGlitter.GetComponent<Glitter>().StartUp(orcaState, approachGlitter, rightHand.GetApproachObj());
        }

        if (leftHand.GetIsApproach())
        {
            if(CanTouch)
                orcaState.ChangeState("G_Approach", leftHand.GetApproachObj());
            else
                orcaCollision.PlayNoEffect();
            //approachGlitter.GetComponent<Glitter>().StartUp(orcaState, approachGlitter, leftHand.GetApproachObj());
        }
    }


}
