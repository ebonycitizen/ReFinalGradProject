using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;
using BehaviorDesigner.Runtime;

public partial class DolphinState : MonoBehaviour
{
    private enum StateEventId
    {
        None,
        Idle,
        Come,
        Swim,
        Jump,
    }

    [SerializeField] //for debug
    private GameObject rayObject;

    private GameObject sendObj;

    [SerializeField]
    private DolphinBoid boid;
    [SerializeField]
    private DolphinSimulation simulation;
    [SerializeField]
    private Animator animator;
    
    private ImtStateMachine<DolphinState> stateMachine;

    // Start is called before the first frame update
    void Awake()
    {
        stateMachine = new ImtStateMachine<DolphinState>(this);

        stateMachine.AddAnyTransition<D_IdleState>((int)StateEventId.Idle);

        stateMachine.AddTransition<D_IdleState, D_ComeState>((int)StateEventId.Come);

        stateMachine.AddTransition<D_IdleState, D_SwimState>((int)StateEventId.Swim);

        stateMachine.AddTransition<D_ComeState, D_SwimState>((int)StateEventId.Swim);

        stateMachine.AddTransition<D_JumpState, D_SwimState>((int)StateEventId.Swim);

        stateMachine.AddTransition<D_SwimState, D_JumpState>((int)StateEventId.Jump);

        stateMachine.SetStartState<D_IdleState>();
    }
    private void Start()
    {
        boid.enabled = false;
        simulation.InitBoid(boid);

        stateMachine.Update();
    }
    private void FixedUpdate()
    {
        stateMachine.Update();
        //Debug.Log(stateMachine.CurrentStateName);
    }
    private void ChangeParentNull()
    {
        transform.parent = null;
    }
    private void ChangeParentSendObj()
    {
        if (sendObj == null)
            return;

        transform.parent = sendObj.transform;
    }
    public bool ChangeState(string tag, GameObject obj)
    {
        sendObj = obj;

        if (tag == "D_Idle")
            stateMachine.SendEvent((int)StateEventId.Idle);
        if (tag == "D_Come")
            stateMachine.SendEvent((int)StateEventId.Come);
        if (tag == "D_Swim")
            stateMachine.SendEvent((int)StateEventId.Swim);
        if (tag == "D_Jump")
            stateMachine.SendEvent((int)StateEventId.Jump);
        return false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Water")
        {
            animator.SetTrigger("Jump");
            stateMachine.SendEvent((int)StateEventId.Jump);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag=="Water")
        {
            stateMachine.SendEvent((int)StateEventId.Swim);
        }
    }
}