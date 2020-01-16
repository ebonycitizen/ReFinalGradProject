using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using DG.Tweening;
public class SparkBgmPart : MonoBehaviour
{
    [SerializeField]
    private AudioSource audio;

    // Start is called before the first frame update
    void Awake()
    {
        //SoundManager.Instance
        //    .ObserveEveryValueChanged(x => x.IsLastBgmStarted)
        //    .Where(x => x)
        //    .Subscribe(_ =>
        //    {
        //        audio.Play();
        //    }).AddTo(this);

        //audio.volume = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        audio.DOFade(1f, 2f);
    }
}
