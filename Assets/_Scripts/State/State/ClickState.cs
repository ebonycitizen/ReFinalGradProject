using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;

public partial class OrcaState
{
    private class ClickState : ImtStateMachine<OrcaState>.State
    {
        private Transform orca;
        private Transform rayObject;
        private ClickEffect click;

        protected internal override void Enter()
        {
            orca = Context.orcaModel.transform;
            rayObject = Context.rayObject.transform;
            click = rayObject.GetComponent<ClickEffect>();

            click.StartEffect(orca);
        }
        protected internal override void Update()
        {
            if (click.hasDone)
                stateMachine.SendEvent((int)StateEventId.Swim);
        }
        protected internal override void Exit()
        {
        }
    }
}
