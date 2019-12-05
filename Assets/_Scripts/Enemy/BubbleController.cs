using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;
using Random = UnityEngine.Random;

public class BubbleController : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem m_bubbleEmmiter;

    [SerializeField]
    private float m_baseIntervalTime = 1;

    [Range(0, 5)]
    [SerializeField]
    private float m_bubbleSize = 1;

    [SerializeField]
    private bool m_enable = false;

    // Start is called before the first frame update
    [Obsolete]
    void Start()
    {
        if (!m_enable)
            return;

        m_bubbleEmmiter.startSize = m_bubbleSize;


        var value = Random.value;

        Observable.Interval(TimeSpan.FromSeconds(m_baseIntervalTime + value))
            .Subscribe(_ =>
            {
                m_bubbleEmmiter.Play();
            }).AddTo(this);

    }
}
