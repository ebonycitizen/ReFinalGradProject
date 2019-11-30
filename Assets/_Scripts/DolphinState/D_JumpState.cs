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
        protected internal override void Enter()
        {
            Context.ChangeParentSendObj();
            parent = Context.sendObj;
            transform = Context.transform;
        }
        protected internal override void Update()
        {

        }
        protected internal override void Exit()
        {

        }
    }
}