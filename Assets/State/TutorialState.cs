using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;
using DG.Tweening;

public partial class OrcaState
{
    public class TutorialState : ImtStateMachine<OrcaState>.State
    {
        protected internal override void Enter()
        {
            Debug.Log("tutrialEnter");
        }
        protected internal override void Update()
        {
            
        }
        protected internal override void Exit()
        {
            Debug.Log("tutrialExit");
        }
    }
}