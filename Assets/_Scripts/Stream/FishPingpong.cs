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

        seq.Append(transform.DOLocalMove(new Vector3(3, 3, 3) , pingPongTime))
            .Append(transform.DOLocalMove(new Vector3(-3, 3, -3), pingPongTime * 2))
            .Append(transform.DOLocalMove(Vector3.zero, pingPongTime));

        seq.Play().SetLoops(-1).SetDelay(Random.value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
