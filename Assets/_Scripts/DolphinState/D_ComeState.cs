using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;

public partial class DolphinState
{
    private class D_ComeState : ImtStateMachine<DolphinState>.State
    {
        private Transform transform;
        protected internal override void Enter()
        {
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