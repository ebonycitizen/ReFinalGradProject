﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;

public partial class OrcaState
{
    private class RescueState : ImtStateMachine<OrcaState>.State
    {
        private Transform orca;
        private Transform rayObject;
        private PathMoveSeq path;

        private Transform rot;
        private float ratio;

        protected internal override void Enter()
        {
            //Context.ChangeParentRayObject();
            orca = Context.orcaModel.transform;
            rayObject = Context.rayObject.transform;
            path = rayObject.GetComponent<PathMoveSeq>();

            Context.ChangeParentNull();

            rot = Context.idleRotation;
            SoundManager.Instance.PlayOneShotDelaySe(ESeTable.Call, 0.5f);
            ratio = 0;
        }
        protected internal override void Update()
        {
            if (path.hasDone)
            {
                if (path.comeDone)
                    stateMachine.SendEvent((int)StateEventId.Idle);

                var dir = Vector3.zero - orca.localPosition;
                var rot = Quaternion.LookRotation(dir);
                orca.localRotation = Quaternion.Lerp(orca.rotation, rot, Time.fixedDeltaTime);

                orca.localPosition += orca.forward * Time.fixedDeltaTime * 4f;

                Context.ChangeParentCameraRig();

                return;
            }

            if (path.hasReach)
            {
                if (ratio < 1f)
                    ratio += Time.fixedDeltaTime;
                orca.position = Vector3.Lerp(orca.position, rayObject.position, ratio);
                orca.rotation = Quaternion.Lerp(orca.rotation, Quaternion.Euler(rayObject.eulerAngles), ratio);
            }
            else
            {
                if (Vector3.Distance(orca.position, rayObject.position) < 10f)
                    path.StartPath();
                orca.position = Vector3.Lerp(orca.position, rayObject.position, Time.fixedDeltaTime * 1f);
                orca.rotation = Quaternion.Lerp(orca.rotation, Quaternion.Euler(rayObject.eulerAngles), Time.fixedDeltaTime * 2f);
            }
        }
        protected internal override void Exit()
        {
            Context.ChangeParentCameraRig();
            
        }
    }
}