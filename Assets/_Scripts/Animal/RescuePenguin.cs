using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RescuePenguin : MonoBehaviour
{
    [SerializeField]
    private Transform seperatePoint;

    private Transform originParent;
    private Sequence s;

    // Start is called before the first frame update
    void Start()
    {
        originParent = transform.parent;

        s = DOTween.Sequence();
        s.Append(transform.DOShakePosition(0.5f, 0.4f)).SetLoops(-1);
        s.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            transform.parent = other.gameObject.transform;
            s.Pause();
        }
        if(other.gameObject.tag == "PenguinStop")
        {
            transform.parent = originParent;
            Destroy(this);
        }
    }
}
