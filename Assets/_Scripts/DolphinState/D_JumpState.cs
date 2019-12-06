using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;

public partial class DolphinState
{
    private class D_JumpState : ImtStateMachine<DolphinState>.State
    {
        private GameObject parent;
        private Transform transform;
        private Vector3 initVec;

        private float time;
        private float gravity=20f;
        protected internal override void Enter()
        {
            Context.ChangeParentSendObj();
            parent = Context.sendObj;
            transform = Context.transform;
            initVec = Context.boid.velocity+new Vector3(0,5,-4);

            time = 0;
        }
        protected internal override void Update()
        {
            var vec = new Vector3(initVec.x-time*3, initVec.y - gravity * time, initVec.z);
            transform.position += vec * Time.fixedDeltaTime;
            time += Time.fixedDeltaTime;

            var rot = Quaternion.LookRotation(vec);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, 0.3f);
        }
        protected internal override void Exit()
        {

        }
    }
}