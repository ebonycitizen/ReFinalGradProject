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
        
        private float ratio;

        private MyCinemachineDollyCart dolly;

        private Transform cameraEye;
        private bool oldHit;

        protected internal override void Enter()
        {
            Context.ChangeParentCameraRig();
            target = Context.idleTarget;
            rot = Context.idleRotation;
            orca = Context.orcaModel.transform;

            cameraEye = Context.cameraEye.transform;

            dolly = Context.dolly;

            forwardPos = Context.cameraRig.transform;

            ratio = 0.25f;
            
            Context.SetBehaviorStatus(true);
        }
        protected internal override void Update()
        {
            //Debug.Log("IdleUpdate");

            Move();
            Rotate();

            if (ratio < 2f)
                ratio += Time.fixedDeltaTime * 0.5f;
#if DEBUG
            if (Input.GetKeyDown(KeyCode.J))
                stateMachine.SendEvent((int)StateEventId.Jump);
            if (Input.GetKeyDown(KeyCode.S))
                stateMachine.SendEvent((int)StateEventId.Swim);
            if (Input.GetKeyDown(KeyCode.R))
                stateMachine.SendEvent((int)StateEventId.PenguinSinging);
            if (Input.GetKeyDown(KeyCode.P))
                stateMachine.SendEvent((int)StateEventId.PlayerJump);
            if (Input.GetKeyDown(KeyCode.T))
                stateMachine.SendEvent((int)StateEventId.Tutorial);
            if (Input.GetKeyDown(KeyCode.K))
                stateMachine.SendEvent((int)StateEventId.Kick);
            if (Input.GetKeyDown(KeyCode.E))
                stateMachine.SendEvent((int)StateEventId.ElectricShock);
            if (Input.GetKeyDown(KeyCode.C))
                stateMachine.SendEvent((int)StateEventId.Come);
            if (Input.GetKeyDown(KeyCode.F))
                stateMachine.SendEvent((int)StateEventId.Follow);
            if (Input.GetKeyDown(KeyCode.N))
                stateMachine.SendEvent((int)StateEventId.None);
            if (Input.GetKeyDown(KeyCode.A))
                stateMachine.SendEvent((int)StateEventId.Approach);
            if (Input.GetKeyDown(KeyCode.Space))
                stateMachine.SendEvent((int)StateEventId.Scroll);
            if (Input.GetKeyDown(KeyCode.LeftAlt))
                stateMachine.SendEvent((int)StateEventId.Attack);
            if (Input.GetKeyDown(KeyCode.L))
                stateMachine.SendEvent((int)StateEventId.LightUp);
#endif

        }

        protected internal override void Exit()
        {
            //Debug.Log("IdleExit");

            Context.SetBehaviorStatus(false);
        }

        private void Move()
        {
            var direction = Quaternion.Euler(target.localEulerAngles) * Context.cameraRig.transform.forward;

            RaycastHit hit;
            Debug.DrawRay(cameraEye.position, direction.normalized * 34);
            int layerMask = 1 << LayerMask.NameToLayer("IgnoreProjection") | 1 << LayerMask.NameToLayer("Stage");
            bool isHit = Physics.Raycast(cameraEye.position, direction, out hit, 34, layerMask);

            Vector3 targetPos;
            if (isHit)
            {
                targetPos = Context.transform.InverseTransformPoint(hit.point - direction * 10);//distance = (transform.position - hit.point).magnitude;
                Debug.Log("targetPos   " + direction * distance + "   " + targetPos);
            }
            else
            {
                targetPos = direction * distance;//distance = maxDistance;
                if (oldHit != isHit)
                    ratio = 1f;
            }

            targetPos -= orca.transform.up *0.3f;

            orca.localPosition = Vector3.Lerp(orca.localPosition, targetPos, Time.fixedDeltaTime * ratio);

            oldHit = isHit;
        }
        private Vector3 old;
        private void Rotate()
        {

            if (dolly != null)
            {
                //orca.rotation = Quaternion.Lerp(orca.rotation, dolly.forward, Time.fixedDeltaTime);
                orca.localRotation = Quaternion.Lerp(orca.localRotation, Quaternion.Euler(dolly.forwardDig.x, dolly.forwardDig.y, rot.localEulerAngles.z), Time.fixedDeltaTime * 3);

                return;
            }
            var d = forwardPos.position - oldForwardPos;
            if (d.magnitude > 0)
            {
                var q = Quaternion.LookRotation(d);

                if (q.eulerAngles != Vector3.zero)
                {
                    //orca.localEulerAngles = new Vector3(q.eulerAngles.x, q.eulerAngles.y, rot.localEulerAngles.z);
                    orca.localRotation = Quaternion.Lerp(orca.localRotation, Quaternion.Euler(q.eulerAngles.x, q.eulerAngles.y, rot.localEulerAngles.z), Time.fixedDeltaTime);
                    
                }

            }

            oldForwardPos = forwardPos.position;

            //var t =  old -orca.localPosition;

            //var rot = orca.localEulerAngles.y;

            //var x = Mathf.Sin(rot) * t.x + Mathf.Cos(rot) *t.z;

            //Debug.Log(x);

            //old = orca.localPosition;

        }


    }
}
