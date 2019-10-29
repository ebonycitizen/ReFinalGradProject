using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;

public partial class OrcaState
{
    private class RescueState : ImtStateMachine<OrcaState>.State
    {
        private Transform orca;
        private Transform rayObject;

        private Transform rot;

        protected internal override void Enter()
        {
            //Context.ChangeParentRayObject();
            orca = Context.orcaModel.transform;
            rayObject = Context.rayObject.transform;

            rot = Context.idleRotation;
        }
        protected internal override void Update()
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                stateMachine.SendEvent((int)StateEventId.Idle);
            }

            orca.position = Vector3.Lerp(orca.position, rayObject.position, Time.fixedDeltaTime * 1f);
            orca.rotation = Quaternion.Lerp(orca.rotation, Quaternion.Euler(rayObject.eulerAngles), Time.fixedDeltaTime * 2f);

        }
        protected internal override void Exit()
        {
            
        }
    }
}