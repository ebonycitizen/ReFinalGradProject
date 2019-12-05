using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;
using UniRx.Triggers;
using SWS;
using System;

public class PenguinJump : PenguinFunction, ITriggerSetupper
{
    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private PenguinAnimatorCtr penguinAnimator;

    [SerializeField]
    private float fowardPower = 10;

    [SerializeField]
    private float upPower = 10;

    [SerializeField]
    private float downPower = 10;

    [SerializeField]
    private float m_timer = 2;

    [SerializeField]
    private splineMove m_splineMove = null;

    [SerializeField]
    private GameObject m_waterSurface = null;

    [SerializeField]
    private ParticleSystem m_waterSurfaceSplash = null;

    public override void Setup()
    {
        penguinAnimator.JumpToSwim();
        rb.AddForce(transform.forward * fowardPower, ForceMode.VelocityChange);
        rb.AddForce(transform.up * upPower, ForceMode.VelocityChange);
        Invoke("AddDownPower", 0.5f);

        Observable.Timer(TimeSpan.FromSeconds(m_timer))
            .Subscribe(_ =>
            {
                rb.useGravity = false;
                rb.isKinematic = true;
                m_splineMove.StartMove();
            }).AddTo(this);
    }
    void AddDownPower()
    {
        rb.AddForce(-transform.up * downPower, ForceMode.VelocityChange);

    }


    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Setup();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == m_waterSurface.name)
        {
            m_waterSurfaceSplash.transform.position = transform.position;
            m_waterSurfaceSplash.Play();
        }

    }
}
