using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;
using DG.Tweening;

public partial class DolphinState
{
    private class D_DefaultSwimState : ImtStateMachine<DolphinState>.State
    {
        DolphinBoid boid;
        DolphinSimulation simulation;

        protected internal override void Enter()
        {
            Context.ChangeParentNull();

            boid = Context.boid;
            boid.enabled = true;

            simulation = Context.simulation;

            simulation.AddBoid(boid);
        }
        protected internal override void Update()
        {
            
        }

        protected internal override void Exit()
        {
            boid.enabled = false;
            simulation.RemoveVoid(boid);
        }
    }
}