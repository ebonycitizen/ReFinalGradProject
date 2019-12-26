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
    private ESeTable m_eSe = ESeTable.Song_1;

    [SerializeField]
    private float m_singTime = 1.6f;

    [SerializeField]
    private float m_turnTime = 1;

    public override void Setup()
    {
        m_penguins.ForEach(x =>
        {
            x.StopAnimation();
            x.PlayNextAnimation("Sing");
        });

        Observable.Timer(TimeSpan.FromSeconds(m_singTime))
            .Subscribe(_ =>
            {
                m_penguins.ForEach(x => x.PlayNextAnimation("Turn"));

            }).AddTo(this);

        //Observable.Timer(TimeSpan.FromSeconds(m_singTime + m_turnTime))
        //    .Subscribe(_ =>
        //    {
        //        m_penguins.ForEach(x => x.StopAnimation());
        //        m_penguins.ForEach(x => x.PlayNextAnimation("Sing"));

        //    }).AddTo(this);

        m_singEffect.Play();
        SoundManager.Instance.PlayOneShot3DSe(m_eSe, m_speaker, 0.6f);
    }

    // Start is called before the first frame update
    void Start()
    {
        //Setup();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
