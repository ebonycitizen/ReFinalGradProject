using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PathMoveSeq : MonoBehaviour
{
    [SerializeField]
    private float duration;
    [SerializeField]
    private float delayTime;
    [SerializeField]
    private Transform pathRef;
    [SerializeField]
    private Ease ease;

    private Sequence s;
    public bool hasReach { get; private set; }
    public bool hasDone { get; private set; }

    [field: SerializeField]
    public bool comeDone { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        Vector3[] movePath = new Vector3[pathRef.childCount];

        for (int i = 0; i < movePath.Length; i++)
        {
            movePath[i] = pathRef.GetChild(i).position;
        }
        hasDone = false;
        hasReach = false;

        s = DOTween.Sequence();
        s.Append(transform.DOPath(movePath, duration, PathType.CatmullRom).SetEase(ease).SetDelay(delayTime).SetLookAt(0.02f, Vector3.forward))
            .AppendCallback(() => hasDone = true);

        StartCoroutine("SetUp");
    }

    IEnumerator SetUp()
    {
        s.Play();
        yield return new WaitForSeconds(0.2f);
        s.Pause();
        yield return null;

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
    //    {
    //        s.Play();
    //        hasReach = true;
    //    }
    //}
    public void StartPath()
    {
        s.Play();
        hasReach = true;
    }

}
