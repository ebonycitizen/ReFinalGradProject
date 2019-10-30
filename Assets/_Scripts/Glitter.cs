using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using HI5;

public class Glitter : MonoBehaviour
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
    private OrcaState orcaState;
    [SerializeField]
    private GameObject sendObj;
    [SerializeField]
    private LayerMask targetLayer;

    [SerializeField]
    private ParticleSystem ripple;
    [SerializeField]
    private ParticleSystem startLockOn;
    [SerializeField]
    private ParticleSystem endLockOn;

    [SerializeField]
    private Color normalColor;
    [SerializeField]
    private Color lockonColor;

    private GameObject cameraTarget;
    private GameObject lockTarget;

    // Start is called before the first frame update
    void Start()
    {
        lockTarget = null;
        startLockOn.Stop();
        endLockOn.Stop();
        ripple.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        cameraTarget = rayCamera.LockOn(targetLayer);
        ChangeEffect();
        StartUp();
        lockTarget = cameraTarget;
    }

    private void StartUp()
    {
        if (cameraTarget != gameObject)
            return;

        if (rightHand.GetVelocity().magnitude >= atkSpeedRequire ||
            leftHand.GetVelocity().magnitude >= atkSpeedRequire || GrabAction.stateDown)
        {
            //HI5_Manager.EnableBothGlovesVibration(400, 400);
            orcaState.ChangeState(gameObject.tag, sendObj);

            foreach (ParticleSystem p in transform.GetComponentsInChildren<ParticleSystem>())
            {
                p.Stop();
            }

            if (endLockOn.isStopped)
                endLockOn.Play();
            Destroy(gameObject,2f);
        }
    }

    private void ChangeEffect()
    {
        if (cameraTarget == gameObject && cameraTarget != lockTarget)
        {
            if (startLockOn.isStopped)
                startLockOn.Play();

            if (ripple.isStopped)
                ripple.Play();

            foreach (ParticleSystem p in transform.GetComponentsInChildren<ParticleSystem>())
            {
                p.startColor = lockonColor;
            }
        }

        if (cameraTarget == null && lockTarget == gameObject)
        {
            if (ripple.isPlaying)
                ripple.Stop();

            foreach (ParticleSystem p in transform.GetComponentsInChildren<ParticleSystem>())
            {
                p.startColor = normalColor;
            }
        }
    }
}
