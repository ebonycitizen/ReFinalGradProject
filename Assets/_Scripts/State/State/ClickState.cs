using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;

public partial class OrcaState
{
    public class ClickState : ImtStateMachine<OrcaState>.State
    {
        private Transform orca;
        private Transform rayObject;

        private Transform rot;

        protected internal override void Enter()
        {
            Context.ChangeParentRayObject();
            orca = Context.orcaModel.transform;
            rayObject = Context.rayObject.transform;

            rot = Context.idleRotation;
            Debug.Log("ClickEnter");
        }
        protected internal override void Update()
        {
            
        }
        protected internal override void Exit()
        {
            
        }
    }
}
