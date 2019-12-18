using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;
using DG.Tweening;

public partial class OrcaState
{
    private class PenguinSingingState : ImtStateMachine<OrcaState>.State
    {
        protected internal override void Enter()
        {
            Context.rayObject.GetComponent<PenguinSing>().Setup();
            stateMachine.SendEvent((int)StateEventId.Idle);

        }

        protected internal override void Update()
        {
        }
        protected internal override void Exit()
        {
        }
    }
}