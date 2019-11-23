using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;

public partial class OrcaState
{
    private class ScrollState : ImtStateMachine<OrcaState>.State
    {
        private Transform orca;

        private MyCinemachineDollyCart dolly;

        private RayFromCamera rayCamera;

        private Vector3 targetPos;

        protected internal override void Enter()
        {
            Context.ChangeParentRayObject();

            orca = Context.orcaModel.transform;

            dolly = Context.dolly;

            rayCamera = Context.cameraEye.GetComponent<RayFromCamera>();

            targetPos = orca.position;
        }
        protected internal override void Update()
        {
            orca.localPosition = Vector3.Lerp(orca.localPosition, Vector3.zero, Time.fixedDeltaTime * 1f);
            orca.localRotation = Quaternion.Lerp(orca.localRotation, Quaternion.Euler(0, 0, 0), Time.fixedDeltaTime * 2f);

        }

        protected internal override void Exit()
        {
            
        }
    }
}