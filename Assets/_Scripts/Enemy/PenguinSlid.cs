using SWS;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using DG.Tweening;
public class PenguinSlid : PenguinFunction, ITriggerSetupper
{
    [SerializeField]
    private GameObject m_waterSurface = null;

    [SerializeField]
    private PenguinAnimatorCtr m_penguinAnimator;

    [SerializeField]
    private ParticleSystem m_waterSurfaceSplash = null;

    [SerializeField]
    private List<ParticleSystem> m_helpEffects = new List<ParticleSystem>();

    [SerializeField]
    private splineMove m_move = null;

    public void PlaySlid()
    {
        transform.DORotate(transform.localEulerAngles - new Vector3(45, 0, 0), 1);
    }
    public void PlayHelpEffect()
    {
        m_helpEffects.ForEach(x => x.Play());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == m_waterSurface.name)
        {
            m_waterSurfaceSplash.transform.position = transform.position;
            m_waterSurfaceSplash.Play();
        }

    }

    public override void Setup()
    {
        m_penguinAnimator.PlayNextAnimation("Walk");
        m_move.StartMove();
    }
}
