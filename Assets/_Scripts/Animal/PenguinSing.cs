using SWS;
using UniRx;
using System;
using UniRx.Triggers;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinSing : PenguinFunction
{
    [SerializeField]
    private List<PenguinAnimatorCtr> m_penguins = new List<PenguinAnimatorCtr>();

    [SerializeField]
    private ParticleSystem m_singEffect = null;

    [SerializeField]
    private Speaker m_speaker = null;

    [SerializeField]
    private ESeTable m_eSe = ESeTable.Tmp_PenguinSinging;

    [SerializeField]
    private float m_singTime = 1.6f;

    [SerializeField]
    private float m_turnTime = 1;

    [SerializeField]
    private AudioSource m_audio = null;

    public override void Setup()
    {
        m_penguins.ForEach(x => x.PlayNextAnimation("Sing"));

        Observable.Timer(TimeSpan.FromSeconds(m_singTime))
            .Subscribe(_ =>
            {
                m_penguins.ForEach(x =>
                {
                    x.PlayAnimation("Turn");
                });

            }).AddTo(this);

        m_singEffect.Play();

        m_audio.volume = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_audio.volume = 1;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
