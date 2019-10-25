using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;

public partial class OrcaState
{
    private class IdleState : ImtStateMachine<OrcaState>.State
    {
        private Transform target;
        private Transform orca;

        private float distance = 7;

        private Transform forwardPos;
        private Vector3 oldForwardPos;

        protected internal override void Enter()
        {
            Context.ChangeParentCameraRig();
            target = Context.idleTarget;
            orca = Context.orcaModel.transform;

            forwardPos = Context.cameraRig.transform;
        }
        protected internal override void Update()
        {
            Debug.Log("IdleUpdate");

            Move();
            Rotate();

            if (Input.GetKeyDown(KeyCode.A))
            {
                stateMachine.SendEvent((int)StateEventId.Jump);
            }
        }

        private void Move()
        {
            var direction = Quaternion.Euler(target.localEulerAngles) * Context.cameraRig.transform.forward;

            var targetPos = direction * distance;

            orca.localPosition = Vector3.Lerp(orca.localPosition, targetPos, Time.fixedDeltaTime * 2f);
        }

        private void Rotate()
        {
            var d = forwardPos.position - oldForwardPos;
            if (d.magnitude > 0)
            {
                var q = Quaternion.LookRotation(d);

                if (q.eulerAngles != Vector3.zero)
                    orca.localEulerAngles = new Vector3(q.eulerAngles.x, q.eulerAngles.y, 0);
            }
            oldForwardPos = forwardPos.position;
        }
        protected internal override void Exit()
        {
            Debug.Log("IdleExit");
        }
    }
}
