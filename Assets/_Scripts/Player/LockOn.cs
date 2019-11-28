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
    private float lockSecMax;

    [SerializeField]
    private float atkSpeedRequire;
    [SerializeField]
    private LayerMask targetLayer;
    [SerializeField]
    private GameObject orca;

    private float lockSec;
    private GameObject cameraTarget;
    private GameObject lockTarget;

    private OrcaState orcaState;
    private OrcaCollision orcaCollision;

    // Start is called before the first frame update
    void Start()
    {
        lockTarget = null;
        lockSec = 0f;
        CanTouch = false;
        orcaState = orca.GetComponent<OrcaState>();
        orcaCollision = orca.GetComponentInChildren<OrcaCollision>();
    }

    // Update is called once per frame
    void Update()
    {
        cameraTarget = rayCamera.LockOn(targetLayer);

        if (cameraTarget == null)
        {
            rayCamera.ResetLockOnImg();
            lockSec = 0f;
        }

        if (cameraTarget != null && cameraTarget == lockTarget)
        {
            lockSec += Time.deltaTime;
            rayCamera.UpdateLockUI(lockSec, lockSecMax);

            //cameraTarget.GetComponent<Glitter>().HitEffect();
            //cameraTarget.GetComponent<Glitter>().ChangeEffect(cameraTarget, lockTarget);
        }

        if (lockSec >= lockSecMax && cameraTarget != null)
        {
            LookAt();
            rayCamera.ResetLockOnImg();
            lockSec = 0f;
        }
        lockTarget = cameraTarget;
    }

    public void CancelApproach()
    {
        if(!CanTouch)
            orcaState.ChangeState("G_Idle", null);
    }

    private void LookAt()
    {
        if(cameraTarget.layer == LayerMask.NameToLayer("Glitter"))
        {
            cameraTarget.GetComponent<Glitter>().HitEffect();
            cameraTarget.GetComponent<Glitter>().StartUp(orcaState, cameraTarget);
        }
        if (cameraTarget.layer == LayerMask.NameToLayer("DolphinGlitter"))
        {
            cameraTarget.GetComponent<DolphinGlitter>().HitEffect();
            cameraTarget.GetComponent<DolphinGlitter>().StartUp(cameraTarget);
        }

    }
}
