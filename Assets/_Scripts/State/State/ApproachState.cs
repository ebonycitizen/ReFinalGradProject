using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;

public partial class OrcaState
{

    private class ApproachState : ImtStateMachine<OrcaState>.State
    {
        private Transform orca;

        private Vector3 pos;

        private Transform forwardPos;
        private Vector3 oldForwardPos;

        protected internal override void Enter()
        {
            orca = Context.orcaModel.transform;
            forwardPos = Context.cameraRig.transform;

            pos = new Vector3(0.85f, 0.5f,-0.05f);
        }
        protected internal override void Update()
        {
            orca.localPosition = Vector3.Lerp(orca.localPosition, pos, Time.fixedDeltaTime / 2);
            Rotate();
            if (Input.GetKeyDown(KeyCode.I))
                stateMachine.SendEvent((int)StateEventId.Idle);
        }

        private void Rotate()
        {
            var d = forwardPos.position - oldForwardPos;
            if (d.magnitude > 0)
            {
                var q = Quaternion.LookRotation(d);

                if (q.eulerAngles != Vector3.zero)
                {
                    orca.localRotation = Quaternion.Lerp(orca.localRotation, Quaternion.Euler(q.eulerAngles.x, q.eulerAngles.y, 0), 0.2f);
                }

            }

            oldForwardPos = forwardPos.position;

        }
        protected internal override void Exit()
        {

        }
    }
}