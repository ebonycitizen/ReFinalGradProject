using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class JellyfishBone : EmissionAction
{
    [SerializeField]
    private float floatingTime = 3;
    [SerializeField]
    private float increasePositionY = 5;


    public override void DoAction()
    {
        float delay = Random.Range(0, floatingTime);

        var sequence = DOTween.Sequence()
            .Append(transform.DOLocalMoveY((transform.localPosition.y + increasePositionY), floatingTime).SetEase(Ease.OutQuart))
            .Append(transform.DOLocalMoveY((transform.localPosition.y), floatingTime * 2));
        sequence.Play().SetLoops(-1).SetDelay(delay);
    }
}
