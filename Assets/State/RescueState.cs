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
        private PathMoveSeq path;

        private Transform rot;
        private float ratio;

        protected internal override void Enter()
        {
            //Context.ChangeParentRayObject();
            orca = Context.orcaModel.transform;
            rayObject = Context.rayObject.transform;
            path = rayObject.GetComponent<PathMoveSeq>();

            rot = Context.idleRotation;
        }
        protected internal override void Update()
        {
            if (path.hasDone)
                stateMachine.SendEvent((int)StateEventId.Idle);

            if (path.hasReach)
            {
                if (ratio < 1f)
                    ratio += Time.fixedDeltaTime;
                orca.position = Vector3.Lerp(orca.position, rayObject.position, ratio);
                orca.rotation = Quaternion.Lerp(orca.rotation, Quaternion.Euler(rayObject.eulerAngles), ratio);
            }
            else
            {
                orca.position = Vector3.Lerp(orca.position, rayObject.position, Time.fixedDeltaTime * 1f);
                orca.rotation = Quaternion.Lerp(orca.rotation, Quaternion.Euler(rayObject.eulerAngles), Time.fixedDeltaTime * 2f);
            }
        }
        protected internal override void Exit()
        {
            
        }
    }
}