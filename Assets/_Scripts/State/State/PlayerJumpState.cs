using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;
using DG.Tweening;

public partial class OrcaState
{
    public class PlayerJumpState : ImtStateMachine<OrcaState>.State
    {
        private Transform orca;
        private Transform player;
        private Transform rot;
        Sequence s;
        
        private Vector3 oldPos;

        private Vector3 pos;


        private MyCinemachineDollyCart dolly;
        private void Rotate()
        {
            if (dolly != null)
            {
                //orca.rotation = Quaternion.Lerp(orca.rotation, dolly.forward, Time.fixedDeltaTime);
                orca.localRotation = Quaternion.Lerp(orca.localRotation, Quaternion.Euler(dolly.forwardDig.x, dolly.forwardDig.y, rot.localEulerAngles.z), Time.fixedDeltaTime * 3);

                return;
            }

        }
        private void Come()
        {
           
        }
        protected internal override void Enter()
        {
            orca = Context.orcaModel.transform;
            player = Context.transform.parent;
            Context.ChangeParentCameraRig();
            rot = Context.idleRotation;
            dolly = Context.dolly;
            Context.orcaAnim.SetTrigger("PlayerJump");
            //Come();

            pos = orca.localPosition;
        }
        protected internal override void Update()
        {
            //Rotate();
            orca.localPosition = Vector3.Lerp(orca.localPosition, pos, Time.fixedDeltaTime);

            Rotate();
        }
        protected internal override void Exit()
        {

        }
    }
}