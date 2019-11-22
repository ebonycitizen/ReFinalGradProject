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

            orca = Context.orcaModel.transform;

            dolly = Context.dolly;

            rayCamera = Context.cameraEye.GetComponent<RayFromCamera>();

            targetPos = orca.position;
        }
        protected internal override void Update()
        {
           Vector3 pos=  rayCamera.ScrollHit("Scroll");
            if(pos!=Vector3.zero)
            {
                targetPos = pos;
            }
            orca.position = Vector3.Lerp(orca.position, targetPos, Time.fixedDeltaTime);


            if (dolly != null)
            {
                //orca.rotation = Quaternion.Lerp(orca.rotation, dolly.forward, Time.fixedDeltaTime);
                orca.localRotation = Quaternion.Lerp(orca.localRotation, Quaternion.Euler(dolly.forwardDig.x, dolly.forwardDig.y, 0), Time.fixedDeltaTime * 3);

                return;
            }
        }

        protected internal override void Exit()
        {
            
        }
    }
}