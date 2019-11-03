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
        float delay = Random.Range(1f, 2f);

        s = DOTween.Sequence();
        s.Append(transform.DOLocalMoveY(transform.localPosition.y + 10, 3).SetEase(Ease.InOutQuad))
            .Append(transform.DOLocalMoveY(transform.localPosition.y, 3).SetEase(Ease.InOutQuad));

        s.Play().SetDelay(delay);

        Rotate(delay);
    }

    private void Rotate(float delay)
    {
        s2 = DOTween.Sequence();
        s2.Append(transform.DOBlendableLocalRotateBy(new Vector3(-45, 0, 0), 2f).SetEase(Ease.InOutQuad))
            .Append(transform.DOBlendableLocalRotateBy(new Vector3(90, 0, 0), 2f).SetEase(Ease.InOutQuad))
            .Append(transform.DOBlendableLocalRotateBy(new Vector3(-45, 0, 0), 2f));

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
