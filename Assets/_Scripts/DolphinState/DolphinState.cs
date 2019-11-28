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
    }

    [SerializeField] //for debug
    private GameObject rayObject;

    private ImtStateMachine<DolphinState> stateMachine;

    // Start is called before the first frame update
    void Awake()
    {
        stateMachine = new ImtStateMachine<DolphinState>(this);

        stateMachine.AddAnyTransition<D_IdleState>((int)StateEventId.Idle);

        stateMachine.AddTransition<D_IdleState, D_ComeState>((int)StateEventId.Idle);

        stateMachine.AddTransition<D_IdleState, D_SwimState>((int)StateEventId.Swim);

        stateMachine.AddTransition<D_ComeState, D_SwimState>((int)StateEventId.Swim);

        stateMachine.SetStartState<D_IdleState>();
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
    public bool ChangeState(string tag, GameObject obj)
    {
        return false;
    }
}