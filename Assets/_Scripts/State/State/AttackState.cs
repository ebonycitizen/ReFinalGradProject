using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;
using DG.Tweening;

public partial class OrcaState
{
    private class AttackState : ImtStateMachine<OrcaState>.State
    {
        private float rotateSec = 0.5f;
        private float atkDist = 10f;
        private Transform orca;
        private Transform rayObject;
        private Animator animator;

        protected internal override void Enter()
        {
            orca = Context.orcaModel.transform;
            animator = orca.GetComponentInChildren<Animator>();
            //rayObject = Context.rayObject.transform;
            
            Sequence s = DOTween.Sequence();

            if(Context.dolly.m_Speed <= 20f)
                atkDist = 5f;

            s.AppendCallback(() => animator.SetTrigger("Attack"))
                .AppendInterval(0.1f)
                .Append(orca.DOLocalMove(orca.localPosition +  orca.forward * atkDist, rotateSec))
                .AppendCallback(()=> stateMachine.PopAndDirectSetState());
            
            s.Play();
        }
        protected internal override void Update()
        {

        }
        protected internal override void Exit()
        {

        }
    }
}
