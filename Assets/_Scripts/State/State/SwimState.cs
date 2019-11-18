using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;

public partial class OrcaState
{
    private class SwimState : ImtStateMachine<OrcaState>.State
    {
        private Transform orca;
        private Transform rayObject;

        private Transform rot;
        private Rigidbody rigid;
        private RigidbodyConstraints constraints;
        protected internal override void Enter()
        {
            Context.ChangeParentRayObject();
            orca = Context.orcaModel.transform;

            rot = Context.idleRotation;
            rigid = orca.GetComponent<Rigidbody>();

            constraints = rigid.constraints;

            rigid.constraints = RigidbodyConstraints.FreezeAll;
        }
        protected internal override void Update()
        {
            orca.localPosition = Vector3.Lerp(orca.localPosition, Vector3.zero, Time.fixedDeltaTime * 1f);
            orca.localRotation= Quaternion.Lerp(orca.localRotation, Quaternion.Euler(0,0, rot.localEulerAngles.z), Time.fixedDeltaTime * 2f);

        }
        protected internal override void Exit()
        {
            rigid.constraints = constraints;
        }
    }
}