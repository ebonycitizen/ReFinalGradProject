using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;

public partial class DolphinState
{
    private class D_ComeState : ImtStateMachine<DolphinState>.State
    {
        private Transform transform;
        private Transform sendObj;
        protected internal override void Enter()
        {
            transform = Context.transform;
            Context.ChangeParentNull();
            sendObj = Context.sendObj.transform;
            SoundManager.Instance.PlayOneShot3DSe(ESeTable.Dolphin_1, Context.speaker);
        }
        protected internal override void Update()
        {
            
            if (Vector3.Distance(transform.position, sendObj.position) > 45f)
            {
                var dir = sendObj.position - (transform.position);
                var rot = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.fixedDeltaTime * 3f);

                transform.position += transform.forward * Time.deltaTime * 20f;
            }
            else
            {
                StateMachine.SendEvent((int)StateEventId.Swim);
            }
        }

        protected internal override void Exit()
        {

        }
    }
}