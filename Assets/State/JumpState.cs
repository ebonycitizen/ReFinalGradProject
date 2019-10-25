using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;

public partial class OrcaState
{
    private class JumpState : ImtStateMachine<OrcaState>.State
    {
        protected internal override void Enter()
        {
            Debug.Log("JumpEnter");
            Context.ChangeParentRayObject();
        }
        protected internal override void Update()
        {
            Debug.Log("JumpUpdate");
            if (Input.GetKeyDown(KeyCode.A))
            {
                stateMachine.SendEvent((int)StateEventId.Idle);
            }
        }
        protected internal override void Exit()
        {
            Debug.Log("JumpExit");
        }
    }
}
