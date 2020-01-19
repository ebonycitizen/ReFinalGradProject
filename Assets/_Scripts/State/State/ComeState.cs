using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;

public partial class OrcaState
{
    private class ComeState : ImtStateMachine<OrcaState>.State
    {

        private Transform orca;
        private Transform rayObject;
        private PathMoveCome path;

        private Transform cameraRig;

        private Vector3 pos;

        private Grab rightHand;
        private Grab leftHand;

        private bool gotoPath;
        protected internal override void Enter()
        {
            orca = Context.orcaModel.transform;
            rayObject = Context.rayObject.transform;
            cameraRig = Context.cameraRig.transform;
            path = rayObject.GetComponent<PathMoveCome>();

            rightHand = Context.rightHand;
            leftHand = Context.leftHand;

            gotoPath = true;

            pos = new Vector3(3, 5);
        }
        protected internal override void Update()
        {
            //if(path.hasDone)
            //{
            //    orca.position = Vector3.Lerp(orca.position, cameraRig.position+pos, Time.fixedDeltaTime);
            //    orca.rotation = Quaternion.Lerp(orca.rotation, Quaternion.Euler(cameraRig.eulerAngles), Time.fixedDeltaTime);

            //    return;
            //}

            if (path.hasDone)
            {
                if (rightHand.FirstContact || Input.GetKeyDown(KeyCode.Return))
                    stateMachine.SendEvent((int)StateEventId.Idle);
            }

            if (!gotoPath)
            {
                orca.position = Vector3.Lerp(orca.position, rayObject.position, Time.fixedDeltaTime);
                orca.rotation = Quaternion.Lerp(orca.rotation, Quaternion.Euler(rayObject.eulerAngles), Time.fixedDeltaTime);

                return;
            }

            if (Vector3.Distance(orca.position, rayObject.position) > 10f)
            {
                var dir = rayObject.position - orca.position;
                var rot = Quaternion.LookRotation(dir);
                orca.rotation = Quaternion.Lerp(orca.rotation, rot, Time.fixedDeltaTime);

                orca.position += orca.forward * Time.deltaTime * 12f;
            }
            else
            {
                gotoPath = false;
                path.StartEvent();
            }
        }
        protected internal override void Exit()
        {
            path.EndEvent();
        }
    }
}