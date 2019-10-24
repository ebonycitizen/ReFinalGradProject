using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ThirdPersonJump : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    private bool canJump;
    Sequence s;
    Sequence s2;

    // Start is called before the first frame update
    void Start()
    {
        canJump = true;

        
        
    }

    private void Jump(Vector3 pos)
    {
        s = DOTween.Sequence();
        s.PrependCallback(() => canJump = false)
            .Append(target.DOLocalMoveY(6, 1).SetEase(Ease.InOutQuad))
            .Append(target.DOLocalMoveY(0, 1).SetEase(Ease.InOutQuad))
            .AppendCallback(() => canJump = true);

        s.Play();
    }

    private void Rotate()
    {
        s2 = DOTween.Sequence();
        s2.Append(target.DOBlendableLocalRotateBy(new Vector3(-45, 0, 0), 0.5f).SetEase(Ease.InOutQuad))
            .Append(target.DOBlendableLocalRotateBy(new Vector3(90, 0, 0), 1f).SetEase(Ease.InOutQuad))
            .Append(target.DOBlendableLocalRotateBy(new Vector3(-45, 0, 0), 1));

        s2.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (canJump && Input.GetKeyDown(KeyCode.A))
        {
            Jump(target.localPosition);
            Rotate();
        }
    }
}
