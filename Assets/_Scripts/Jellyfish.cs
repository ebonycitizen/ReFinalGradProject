using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Jellyfish : MonoBehaviour
{
    [SerializeField]
    private float floatingTime = 3;
    [SerializeField]
    private float increasePositionY = 5;
    [SerializeField]
    private float dushSpeed;

    private Tween tween;

    // Start is called before the first frame update
    void Start()
    {
        float delay = Random.Range(0, floatingTime);

        var sequence = DOTween.Sequence()
            .Append(transform.DOLocalMoveY((transform.localPosition.y + increasePositionY), floatingTime).SetEase(Ease.OutQuart))
            .Append(transform.DOLocalMoveY((transform.localPosition.y), floatingTime * 2));
            
        sequence.Play().SetLoops(-1).SetDelay(delay);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Dush()
    {
        if (tween != null)
            tween.timeScale = dushSpeed;
    }

    public void Rest()
    {
        if (tween != null)
            tween.timeScale = 1;
    }
}
