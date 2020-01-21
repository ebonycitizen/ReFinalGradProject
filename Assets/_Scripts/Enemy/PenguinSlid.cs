using SWS;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using DG.Tweening;
using BehaviorDesigner.Runtime;
using UnityEngine.AI;

public class PenguinSlid : PenguinFunction, ITriggerSetupper
{
    [SerializeField]
    private PenguinAnimatorCtr m_penguinAnimator = null;

    [SerializeField]
    private splineMove m_move = null;

    [SerializeField]
    private float m_delayTime = 0;

    [SerializeField]
    private BehaviorTree m_behavior = null;

    [SerializeField]
    private NavMeshAgent m_agent = null;

    public void PlayWalk()
    {
        m_penguinAnimator.StopAnimation();
        m_penguinAnimator.PlayNextAnimation("Walk");
        m_move.Stop();
        m_move.StopAllCoroutines();

        m_agent.enabled = true;

        m_behavior.EnableBehavior();

    }

    public void PlaySlid()
    {
        transform.DORotate(transform.localEulerAngles - new Vector3(45, 0, 0), 1);
    }

    public void Resume()
    {
        transform.DORotate(transform.localEulerAngles + new Vector3(45, 0, 0), 1.6f);
    }
    public override void Setup()
    {
        m_agent.enabled = false;


        Observable.Timer(TimeSpan.FromSeconds(m_delayTime))
            .Subscribe(_ =>
            {
                m_penguinAnimator.PlayNextAnimation("Walk");
                m_move.StartMove();

            }).AddTo(this);

    }
}
