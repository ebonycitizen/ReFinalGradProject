using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;

public partial class OrcaState
{
    public class FollowState : ImtStateMachine<OrcaState>.State
    {

        private Transform target;
        private Transform orca;

        private float distance = 7;

        private Vector3 pos;

        private Transform cameraEye;

        private FollowEvent followEvent;

        protected internal override void Enter()
        {
            orca = Context.orcaModel.transform;

            target = Context.idleTarget;

            cameraEye = Context.cameraEye.transform;

            followEvent = Context.rayObject.GetComponent<FollowEvent>();

            followEvent.StartEvent();
        }
        protected internal override void Update()
        {
            var direction = Quaternion.Euler(target.localEulerAngles) * Context.cameraRig.transform.forward;
            var targetPos = cameraEye.position +direction * 32;


            var dir =  targetPos - orca.position;

            var rot = Quaternion.LookRotation(dir);

            var r = rot * Quaternion.Inverse(orca.rotation);

            var force = new Vector3(r.x, r.y, r.z).magnitude;

            Debug.Log(Vector3.Distance(orca.position, targetPos));

            var dis = Vector3.Distance(orca.position, targetPos);

            if (dis< 5f)
            {
                orca.position = Vector3.Lerp(orca.position, targetPos, Time.fixedDeltaTime / 2f);
                orca.rotation = Quaternion.Lerp(orca.rotation, Quaternion.LookRotation(cameraEye.position - orca.position), Time.fixedDeltaTime/2);
            }
            else if (dis < 13f)
            {
                orca.rotation = Quaternion.Lerp(orca.rotation, rot, Time.fixedDeltaTime * force * 3);
                orca.position += orca.forward * Time.deltaTime * (5f + dis / 3);
            }
            else
            {
                orca.rotation = Quaternion.Lerp(orca.rotation, rot, Time.fixedDeltaTime * force * 3);

                orca.position += orca.forward * Time.deltaTime * (12f + dis / 10);
            }
        }
        protected internal override void Exit()
        {
            followEvent.EndEvent();
        }
    }
}
