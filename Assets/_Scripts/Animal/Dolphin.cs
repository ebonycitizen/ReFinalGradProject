using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWS;
using DG.Tweening;

public class Dolphin : MonoBehaviour
{
    [SerializeField]
    private float dushSpeed;
    [SerializeField]
    private float restSpeed;

    [SerializeField]
    private GameObject bigSplash;
    [SerializeField]
    private GameObject waterSplash;

    private float normalSpeed;
    private splineMove splineMove;
    private Animator animator;
    private ContactPoint[] c;
    private Vector3 oldPos;

    private Sequence s;
    private Sequence s2;

    // Start is called before the first frame update
    void Start()
    {
        splineMove = GetComponentInParent<splineMove>();
        normalSpeed = splineMove.speed;

        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Dush()
    {
        splineMove.ChangeSpeed(dushSpeed);
        animator.speed = 3f;
    }

    public void Rest()
    {
        splineMove.ChangeSpeed(restSpeed);
        animator.speed = 0.5f;
    }

    public void Normal()
    {
        splineMove.ChangeSpeed(normalSpeed);
        animator.speed = 1f;
    }

    public void Jump()
    {
        float delay = Random.Range(0f, 1f);

        s = DOTween.Sequence();
        s.Append(transform.DOLocalMoveY(transform.localPosition.y + 9, 2.5f).SetEase(Ease.InOutQuad))
            .AppendInterval(0.7f)
            .Append(transform.DOLocalMoveY(transform.localPosition.y, 3.5f).SetEase(Ease.InOutQuad));

        s.Play().SetDelay(delay);

        Rotate(delay);
    }

    private void Rotate(float delay)
    {
        s2 = DOTween.Sequence();

        s2.Append(transform.DOLocalRotate(new Vector3(-45, 0, 0), 0.5f).SetEase(Ease.InOutQuad))
            .AppendInterval(delay)
            .AppendCallback(() => animator.speed = 1f)
            .AppendCallback(() => animator.SetTrigger("Jump"))
            .Append(transform.DOLocalRotate(new Vector3(0, 0, 0), 1f).SetEase(Ease.InOutQuad))
            .Append(transform.DOLocalRotate(new Vector3(45, 0, 0), 0.5f).SetEase(Ease.InOutQuad))
            .AppendInterval(2.5f)
            .Append(transform.DOLocalRotate(new Vector3(0, 0, 0),1f).SetEase(Ease.InOutQuad));

        s2.Play().SetDelay(delay);
    }

    private void OnCollisionEnter(Collision collision)
    {
        c = collision.contacts;
        if (collision.gameObject.tag == "Water")
            Instantiate(bigSplash, c[0].point, bigSplash.transform.rotation);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            Instantiate(bigSplash, c[0].point, bigSplash.transform.rotation);
            Instantiate(waterSplash, transform);
        }
    }
}
