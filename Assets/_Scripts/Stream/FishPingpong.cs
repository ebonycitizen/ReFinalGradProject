using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FishPingpong : MonoBehaviour
{
    private float pingPongTime = 2.5f;
    // Start is called before the first frame update
    void Start()
    {
        var seq = DOTween.Sequence();

        seq.Append(transform.DOLocalMoveY(20, pingPongTime))
            .Append(transform.DOLocalMoveY( -20, pingPongTime * 2))
            .Append(transform.DOLocalMoveY(0, pingPongTime));

        seq.Play().SetLoops(-1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
