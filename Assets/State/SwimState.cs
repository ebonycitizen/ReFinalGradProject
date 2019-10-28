using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;

public partial class OrcaState
{
    public class SwimState : ImtStateMachine<OrcaState>.State
    {
        private Transform orca;

        protected internal override void Enter()
        {
            Debug.Log("SwimEnter");
            Context.ChangeParentRayObject();
        }
        protected internal override void Update()
        {
            Debug.Log("SwimUpdate");
            if (Input.GetKeyDown(KeyCode.S))
            {
                stateMachine.SendEvent((int)StateEventId.Idle);
            }

            orca.localPosition = Vector3.Lerp(orca.localPosition, Vector3.zero, Time.fixedDeltaTime * 2f);
        }
        protected internal override void Exit()
        {
            Debug.Log("SwimExit");
        }
    }
}