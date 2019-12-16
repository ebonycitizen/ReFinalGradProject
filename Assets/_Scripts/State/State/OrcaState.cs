using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;
using BehaviorDesigner.Runtime;

public partial class OrcaState : MonoBehaviour
{
    private enum StateEventId
    {
        None,
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
        Follow,
        Approach,
        Scroll,
        Attack,
        LightUp,
    }

    private ImtStateMachine<OrcaState> stateMachine;

    [SerializeField]
    private GameObject cameraRig;
    [SerializeField]
    private GameObject cameraEye;
    [SerializeField]
    private Grab rightHand;
    [SerializeField]
    private Grab leftHand;

    [SerializeField] //for debug
    private GameObject rayObject;

    [SerializeField]
    private GameObject orcaModel;

    [SerializeField]
    private Transform idleTarget;
    [SerializeField]
    private Transform idleRotation;

    [SerializeField]
    private Animator orcaAnim;
    [SerializeField]
    private BehaviorTree m_changeAnimation;

    [SerializeField]
    private MyCinemachineDollyCart dolly;

    private void Awake()
    {
        stateMachine = new ImtStateMachine<OrcaState>(this);

        stateMachine.AddAnyTransition<IdleState>((int)StateEventId.Idle);


        stateMachine.AddTransition<IdleState, SwimState>((int)StateEventId.Swim);
        stateMachine.AddTransition<IdleState, RescueState>((int)StateEventId.Rescue);
        stateMachine.AddTransition<IdleState, KickState>((int)StateEventId.Kick);
        stateMachine.AddTransition<IdleState, ElectricShock>((int)StateEventId.ElectricShock);
        stateMachine.AddTransition<IdleState, ApproachState>((int)StateEventId.Approach);
        stateMachine.AddTransition<IdleState, NoneState>((int)StateEventId.None);
        stateMachine.AddTransition<IdleState, LightUpState>((int)StateEventId.LightUp);

        stateMachine.AddTransition<IdleState, JumpState>((int)StateEventId.Jump);
        stateMachine.AddTransition<ApproachState, JumpState>((int)StateEventId.Jump);

        //stateMachine.AddTransition<IdleState, FollowState>((int)StateEventId.Follow);
        //stateMachine.AddTransition<TutorialState, FollowState>((int)StateEventId.Follow);

        //stateMachine.AddTransition<IdleState, ComeState>((int)StateEventId.Come);
        stateMachine.AddTransition<TutorialState, ComeState>((int)StateEventId.Come);

        stateMachine.AddTransition<IdleState, TutorialState>((int)StateEventId.Tutorial);
        stateMachine.AddTransition<NoneState, TutorialState>((int)StateEventId.Tutorial);

        stateMachine.AddTransition<SwimState, PlayerJumpState>((int)StateEventId.PlayerJump);

        stateMachine.AddTransition<SwimState, ClickState>((int)StateEventId.Click);
        stateMachine.AddTransition<ClickState, SwimState>((int)StateEventId.Swim);

        stateMachine.AddTransition<IdleState, ScrollState>((int)StateEventId.Scroll);

        stateMachine.AddTransition<IdleState, AttackState>((int)StateEventId.Attack);
        stateMachine.AddTransition<ScrollState, AttackState>((int)StateEventId.Attack);

        stateMachine.SetStartState<NoneState>();
#if DEBUG
        stateMachine.SetStartState<IdleState>();
#endif

    }

    private void Start()
    {
        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.Update();
        //Debug.Log(stateMachine.CurrentStateName);
    }

    private void ChangeParentCameraRig()
    {
        if (cameraRig != null)
            orcaModel.transform.parent = transform;
    }

    private void ChangeParentNull()
    {
        orcaModel.transform.parent = null;
    }

    private void ChangeParentRayObject()
    {
        if (rayObject != null)
            orcaModel.transform.parent = rayObject.transform;
    }

    public void SetBehaviorStatus(bool status)
    {
        if (status)
            m_changeAnimation.EnableBehavior();
        else
            m_changeAnimation.DisableBehavior();
    }

    public bool IsIdleState()
    {
        return stateMachine.IsCurrentState<IdleState>();
    }


    public bool ChangeState(string tag, GameObject obj)
    {
        if (obj == null)
            this.rayObject = orcaModel;

        this.rayObject = obj;

        if (tag == "G_Approach" && stateMachine.CurrentStateName == "IdleState")
        {
            stateMachine.SendEvent((int)StateEventId.Approach);
            return true;
        }
        if (tag == "G_Jump" && stateMachine.CurrentStateName == "IdleState")
        {
            stateMachine.SendEvent((int)StateEventId.Jump);
            return true;
        }
        if (tag == "G_Rescue" && stateMachine.CurrentStateName == "IdleState")
        {
            stateMachine.SendEvent((int)StateEventId.Rescue);
            return true;
        }
        if (tag == "G_ElectricShock" && stateMachine.CurrentStateName == "IdleState")
        {
            stateMachine.SendEvent((int)StateEventId.ElectricShock);
            return true;
        }
        if (tag == "G_Kick" && stateMachine.CurrentStateName == "IdleState")
        {
            stateMachine.SendEvent((int)StateEventId.Kick);
            return true;
        }
        if (tag == "G_Swim" && stateMachine.CurrentStateName == "IdleState")
        {
            stateMachine.SendEvent((int)StateEventId.Swim);
            return true;
        }
        if (tag == "G_Scroll" && stateMachine.CurrentStateName == "IdleState")
        {
            stateMachine.SendEvent((int)StateEventId.Scroll);
            return true;
        }
        if (tag == "G_LightUp" && stateMachine.CurrentStateName == "IdleState")
        {
            stateMachine.SendEvent((int)StateEventId.LightUp);
            return true;
        }


        if (tag == "G_Click" && stateMachine.CurrentStateName == "SwimState")
        {
            stateMachine.SendEvent((int)StateEventId.Click);
            return true;
        }

        if (tag == "G_Attack")
        {
            stateMachine.PushState();
            stateMachine.SendEvent((int)StateEventId.Attack);
            return true;
        }

        if (tag == "G_Tutorial")
        {
            stateMachine.SendEvent((int)StateEventId.Tutorial);
            return true;
        }
        if (tag == "G_Idle")
        {
            stateMachine.SendEvent((int)StateEventId.Idle);
            return true;
        }
        if (tag == "G_PlayerJump")
        {
            stateMachine.SendEvent((int)StateEventId.PlayerJump);
            return true;
        }
        if (tag == "G_Come")
        {
            stateMachine.SendEvent((int)StateEventId.Come);
            return true;
        }
        if (tag == "G_Click")
        {
            stateMachine.SendEvent((int)StateEventId.Click);
            return true;
        }
        return false;
    }
}
