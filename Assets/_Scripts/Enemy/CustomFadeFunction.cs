using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;
using UnityEngine;
using DG.Tweening;
using UniRx;
public class CustomFadeFunction : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_textMeshPro;


    [SerializeField]
    private float m_waitTime = 0;

    [SerializeField]
    private float m_nextTime = 0;

    [SerializeField]
    private float m_duration = 3;

    // Start is called before the first frame update
    void Start()
    {
        Observable.Timer(TimeSpan.FromSeconds(m_waitTime)).
            Subscribe(_ =>
            {
                m_textMeshPro.DOFade(1, m_duration+0.5f);
            }).AddTo(this);

        Observable.Timer(TimeSpan.FromSeconds(m_nextTime)).
            Subscribe(_ =>
            {
                m_textMeshPro.DOFade(0, m_duration);
            }).AddTo(this);

    }
}
