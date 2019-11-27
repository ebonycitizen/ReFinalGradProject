using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;

public partial class OrcaState
{
    private class RescueState : ImtStateMachine<OrcaState>.State
    {
        private Transform orca;
        private Transform rayObject;
        private PathMoveSeq path;

        private Transform rot;
        private float ratio;
        private int gotoIdle;

        protected internal override void Enter()
        {
            //Context.ChangeParentRayObject();
            orca = Context.orcaModel.transform;
            rayObject = Context.rayObject.transform;
            path = rayObject.GetComponent<PathMoveSeq>();

            Context.ChangeParentNull();

            rot = Context.idleRotation;
            SoundManager.Instance.PlayOneShotDelaySe(ESeTable.Orac_7, 0.5f);
            ratio = 0;
            gotoIdle = 0;

            Context.orcaAnim.SetTrigger("Rescue");
        }
        protected internal override void Update()
        {
            if (path.hasDone)
            {

                Context.ChangeParentCameraRig();
                if (path.comeDone)
                {

                    if (Vector3.Distance(orca.localPosition, new Vector3(-2, 0, 1)) < 5f)
                        stateMachine.SendEvent((int)StateEventId.Idle);
                    

                    orca.localPosition = Vector3.Lerp(orca.localPosition, new Vector3(-2, 0, 1), Time.fixedDeltaTime);
                    orca.localRotation = Quaternion.Lerp(orca.localRotation, Quaternion.identity, Time.fixedDeltaTime*2);
                    return;
                }
                else
                {

                    var targetPos = Vector3.zero;
                    var speed = 0;
                    switch(gotoIdle)
                    {
                        case 0:
                            targetPos = new Vector3(-2, 0, 0);
                            speed = 6;
                            break;
                        case 1:
                            targetPos = new Vector3(0, 0, -3);
                            speed = 3;
                            break;
                        case 2:
                            targetPos = new Vector3(3, 0, 1.5f);
                            speed = 2;
                            break;
                        case 3:
                            stateMachine.SendEvent((int)StateEventId.Idle);
                            break;
                    }

                    if (Vector3.Distance(orca.localPosition, targetPos) < 2f)
                        gotoIdle++;

                    var dir = (targetPos - orca.localPosition).normalized;
                    var rot = Quaternion.LookRotation(dir);
                    orca.localRotation = Quaternion.Lerp(orca.localRotation, rot, Time.fixedDeltaTime*2);

                    orca.localPosition += orca.forward * Time.fixedDeltaTime * speed;

                    //orca.localPosition = Vector3.Lerp(orca.localPosition, targetPos, Time.fixedDeltaTime);
                    //orca.localRotation = Quaternion.Lerp(orca.localRotation, Quaternion.Euler(targetRot), Time.fixedDeltaTime*3);
                    return;
                }

                
            }

            if (path.hasReach)
            {
                if (ratio < 1f)
                    ratio += Time.fixedDeltaTime * 0.25f;
                orca.position = Vector3.Lerp(orca.position, rayObject.position, ratio);
                orca.rotation = Quaternion.Lerp(orca.rotation, Quaternion.Euler(rayObject.eulerAngles), ratio);
            }
            else
            {
                if (Vector3.Distance(orca.position, rayObject.position) < 10f)
                {
                    Context.orcaAnim.SetTrigger("Idle");
                    path.StartPath();
                }
                orca.position = Vector3.Lerp(orca.position, rayObject.position, Time.fixedDeltaTime * 1f);
                orca.rotation = Quaternion.Lerp(orca.rotation, Quaternion.Euler(rayObject.eulerAngles), Time.fixedDeltaTime * 3f);
            }
        }
        protected internal override void Exit()
        {
            Context.ChangeParentCameraRig();
            
        }
    }
}