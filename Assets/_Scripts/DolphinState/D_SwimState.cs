using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;
using DG.Tweening;

public partial class DolphinState
{
    private class D_SwimState : ImtStateMachine<DolphinState>.State
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
            Shout();
        }
        protected internal override void Update()
        {
            
        }

        protected internal override void Exit()
        {
            boid.enabled = false;
            simulation.RemoveVoid(boid);
        }
        
        private void Shout()
        {
            var waitTime = Random.Range(15, 25);
            var seRand = Random.Range(0, 2);
            ESeTable se = ESeTable.Dolphin_2;

            if(seRand == 0)
                se = ESeTable.Dolphin_1;
            else if (seRand >= 1)
                se = ESeTable.Dolphin_2;

            Sequence s = DOTween.Sequence();

            s.AppendInterval(waitTime)
                .AppendCallback(() => SoundManager.Instance.PlayOneShot3DSe(se, Context.speaker))
                .AppendCallback(() => Context.animator.SetTrigger("Shout"));

            s.Play().SetLoops(-1);
        }

    }
}