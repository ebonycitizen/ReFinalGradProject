using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;

public partial class OrcaState
{
    private class IdleState : ImtStateMachine<OrcaState>.State
    {
        private Transform target;
        private Transform rot;
        private Transform orca;

        private float distance = 7;

        private Transform forwardPos;
        private Vector3 oldForwardPos;

        protected internal override void Enter()
        {
            Context.ChangeParentCameraRig();
            target = Context.idleTarget;
            rot = Context.idleRotation;
            orca = Context.orcaModel.transform;

            forwardPos = Context.cameraRig.transform;
        }
        protected internal override void Update()
        {
            Debug.Log("IdleUpdate");

            Move();
            Rotate();

            if (Input.GetKeyDown(KeyCode.J))
                stateMachine.SendEvent((int)StateEventId.Jump);
            if(Input.GetKeyDown(KeyCode.S))
                stateMachine.SendEvent((int)StateEventId.Swim);
            if (Input.GetKeyDown(KeyCode.R))
                stateMachine.SendEvent((int)StateEventId.Rescue);
            if (Input.GetKeyDown(KeyCode.P))
                stateMachine.SendEvent((int)StateEventId.PlayerJump);
            if (Input.GetKeyDown(KeyCode.T))
                stateMachine.SendEvent((int)StateEventId.Tutorial);
        }

        private void Move()
        {
            var direction = Quaternion.Euler(target.localEulerAngles) * Context.cameraRig.transform.forward;

            var targetPos = direction * distance;

            orca.localPosition = Vector3.Lerp(orca.localPosition, targetPos, Time.fixedDeltaTime * 2f);
        }
        private Vector3 old;
        private void Rotate()
        {
            var d = forwardPos.position - oldForwardPos;
            if (d.magnitude > 0)
            {
                var q = Quaternion.LookRotation(d);

                if (q.eulerAngles != Vector3.zero)
                {
                    //orca.localEulerAngles = new Vector3(q.eulerAngles.x, q.eulerAngles.y, rot.localEulerAngles.z);
                    orca.localRotation = Quaternion.Lerp(orca.localRotation, Quaternion.Euler(q.eulerAngles.x, q.eulerAngles.y, rot.localEulerAngles.z),0.2f);
                }
                
            }

            oldForwardPos = forwardPos.position;

            //var t =  old -orca.localPosition;

            //var rot = orca.localEulerAngles.y;

            //var x = Mathf.Sin(rot) * t.x + Mathf.Cos(rot) *t.z;

            //Debug.Log(x);

            //old = orca.localPosition;
             
        }
        protected internal override void Exit()
        {
            Debug.Log("IdleExit");
        }
    }
}
