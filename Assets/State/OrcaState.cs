using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;

public partial class OrcaState : MonoBehaviour
{
    private enum StateEventId
    {
        Idle,
        Jump,
        Swim,
        Click,
        Come,
        Rescue,
        PlayerJump,
        Tutorial,
        Kick,
        ElectricShock,

    }
   
    private ImtStateMachine<OrcaState> stateMachine;

    [SerializeField]
    private GameObject cameraRig;

    [SerializeField] //for debug
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
            stateMachine.AddTransition<IdleState, SwimState>((int)StateEventId.Swim);
            stateMachine.AddTransition<IdleState, RescueState>((int)StateEventId.Rescue);
            stateMachine.AddTransition<IdleState, PlayerJumpState>((int)StateEventId.PlayerJump);
            stateMachine.AddTransition<IdleState, TutorialState>((int)StateEventId.Tutorial);
            stateMachine.AddTransition<TutorialState, TutorialState>((int)StateEventId.Tutorial);
            stateMachine.AddTransition<IdleState, KickState>((int)StateEventId.Kick);
            stateMachine.AddTransition<IdleState, ElectricShock>((int)StateEventId.ElectricShock);


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
            orcaModel.transform.parent = transform;
    }

    private void ChangeParentRayObject()
    {
        if (rayObject != null)
            orcaModel.transform.parent = rayObject.transform;
    }

    public bool ChangeState(string tag, GameObject obj)
    {
        if (obj == null || stateMachine.CurrentStateName != "IdleState")
            return false;

        this.rayObject = obj;

        if (tag == "G_Jump")
            stateMachine.SendEvent((int)StateEventId.Jump);
        if (tag == "G_Rescue")
            stateMachine.SendEvent((int)StateEventId.Rescue);

        return true;
    }
}
