using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;
using DG.Tweening;

public partial class OrcaState
{
    private class KickState : ImtStateMachine<OrcaState>.State
    {
        private Transform orca;
        private Transform rayObject;
        private Animator animator;

        private void MoveForward()
        {
            orca.position = Vector3.Lerp(orca.position, rayObject.position + orca.forward * 20, Time.fixedDeltaTime);
        }

        protected internal override void Enter()
        {
            orca = Context.orcaModel.transform;
            rayObject = Context.rayObject.transform;
            animator = orca.GetComponentInChildren<Animator>();

            Sequence s = DOTween.Sequence();

            s.AppendInterval(1f)
                .AppendCallback(()=>animator.SetTrigger("Kick"))
                .AppendCallback(() => animator.speed = 10f)
                .AppendCallback(() => rayObject.GetComponent<CrabBall>().Kick())
                .AppendInterval(1.2f)
                .AppendCallback(() => animator.SetTrigger("Idle"))
                .AppendCallback(()=> stateMachine.SendEvent((int)StateEventId.Idle));

            s.Play();
            
        }
        protected internal override void Update()
        {
            MoveForward();
        }
        protected internal override void Exit()
        {
            animator.speed = 1f;
        }
    }
}
