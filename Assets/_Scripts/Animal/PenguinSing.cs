using SWS;
using UniRx;
using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PenguinSing : PenguinFunction
{
    [SerializeField]
    private List<PenguinAnimatorCtr> m_penguins = new List<PenguinAnimatorCtr>();

    [SerializeField]
    private ParticleSystem m_singEffect = null;

    [SerializeField]
    private float m_singTime = 1.6f;

    [SerializeField]
    private AudioSource m_audio = null;

    [SerializeField]
    private float m_stopAudioTime = 3;

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

        Observable.Timer(TimeSpan.FromSeconds(m_stopAudioTime))
    .Subscribe(_ =>
    {
        StopSing();

    }).AddTo(this);

        m_singEffect.Play();

        m_audio.volume = 1;
    }

    private void StopSing()
    {
        Sequence s = DOTween.Sequence();
        s.Append(m_audio.DOFade(0f, 1f))
            .AppendInterval(1f)
            .AppendCallback(() => m_audio.Stop());
        s.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        //m_audio.volume = 0;
        SoundManager.Instance
    .ObserveEveryValueChanged(x => x.IsBgmStarted)
    .Where(x => x)
    .Subscribe(_ =>
    {
        m_audio.Play();
    }).AddTo(this);

    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.F1))
        //{
        //    Setup();
        //}


    }
}
