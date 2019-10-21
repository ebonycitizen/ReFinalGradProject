using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyFade : MonoBehaviour
{
    [SerializeField]
    private float delayTime;

    private float moveTime;
    private float fadeTime;

    [SerializeField]
    private float floatingTime=3;
    [SerializeField]
    private float increasePositionY = 5;

    private float initPosY = 0;
    private float randomRotationValue = 5;
    private float rotateStartTime=2;


    // Start is called before the first frame update
    void Start()
    {
        float time = Random.Range(0, floatingTime);

        var sequence = DOTween.Sequence()
            .AppendInterval(time)
            .Append(transform.DOLocalMoveY((transform.localPosition.y + increasePositionY), floatingTime).SetEase(Ease.OutQuad))
            .Append(transform.DOLocalMoveY((transform.localPosition.y), floatingTime * 2).SetEase(Ease.InOutQuad));

        sequence.Play().SetLoops(-1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
