using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyPathMoveEvent : MonoBehaviour
{
    [SerializeField]
    private float moveTime;
    [SerializeField]
    private float delayTime;
    [SerializeField]
    private Transform pathRef;
    [SerializeField]
    private Ease ease;

    // Start is called before the first frame update
    void Start()
    {
        Vector3[] movePath = new Vector3[pathRef.childCount - 1];

        for (int i = 0; i < movePath.Length; i++)
        {
            movePath[i] = pathRef.GetChild(i).localPosition;
        }

        transform.DOLocalPath(movePath, moveTime, PathType.CatmullRom)
           .SetEase(ease).SetDelay(delayTime).SetLookAt(0.05f, Vector3.forward)
           .OnComplete(() => DestroyObj()).GotoWaypoint(0, true);

        StartCoroutine("rotate");
    }

    IEnumerator rotate()
    {
        yield return new WaitForSeconds(delayTime);

        while(transform.eulerAngles.z<350)
        {
            transform.eulerAngles += new Vector3(0, 0, 1);
            yield return null;
        }

        yield break;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void DestroyObj()
    {
        //Destroy(gameObject);
    }
}
