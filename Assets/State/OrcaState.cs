using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;

public partial class OrcaState : MonoBehaviour
{
   private enum StateEventId
    {
        Idle,
        Eat,
        Jump,
        Collect,
        Swim,
        Click,
        Come,
    }

    private ImtStateMachine<OrcaState> stateMachine;

    [SerializeField]
    private GameObject cameraRig;
    [SerializeField]
    private GameObject rayObject;

    [SerializeField]
    private GameObject orcaModel;

    [SerializeField]
    private Transform idleTarget;
    [SerializeField]
    private Transform idleRotation;

    private void Awake()
    {
        {
            stateMachine = new ImtStateMachine<OrcaState>(this);
            stateMachine.AddAnyTransition<IdleState>((int)StateEventId.Idle);
            stateMachine.AddTransition<IdleState, JumpState>((int)StateEventId.Jump);

            stateMachine.SetStartState<IdleState>();
        }
    }

    private void Start()
    {
        stateMachine.Update();
    }
    private void FixedUpdate()
    {
        stateMachine.Update();
    }

    private void ChangeParentCameraRig()
    {
        if (cameraRig != null)
            transform.parent = cameraRig.transform;
    }

    private void ChangeParentRayObject()
    {
        if (rayObject != null)
            transform.parent = rayObject.transform;
    }

}
