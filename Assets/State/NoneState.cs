using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;

public partial class OrcaState
{
    private class NoneState : ImtStateMachine<OrcaState>.State
    {
        private Transform orca;
        private Transform rayObject;

        protected internal override void Enter()
        {
            orca = Context.orcaModel.transform;
            rayObject = Context.rayObject.transform;
            orca.position = rayObject.position;
        }
        protected internal override void Update()
        {
            
        }
        protected internal override void Exit()
        {
           
        }
    }
}
