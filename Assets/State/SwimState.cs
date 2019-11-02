﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;

public partial class OrcaState
{
    public class SwimState : ImtStateMachine<OrcaState>.State
    {
        private Transform orca;
        private Transform rayObject;

        private Transform rot;

        protected internal override void Enter()
        {
            Context.ChangeParentRayObject();
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

            orca.localPosition = Vector3.Lerp(orca.localPosition, Vector3.zero, Time.fixedDeltaTime * 1f);
            orca.localRotation= Quaternion.Lerp(orca.localRotation, Quaternion.Euler(0,0, rot.localEulerAngles.z), Time.fixedDeltaTime * 2f);
            
        }
        protected internal override void Exit()
        {
        }
    }
}