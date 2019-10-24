using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyPathMove : MonoBehaviour
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
        Vector3[] movePath = new Vector3[pathRef.childCount];

        for (int i = 0; i < movePath.Length; i++)
        {
            movePath[i] = pathRef.GetChild(i).position;
        }

        transform.DOPath(movePath, moveTime, PathType.CatmullRom)
       .SetEase(ease).SetDelay(delayTime)
       .OnComplete(() => DestroyObj()).SetLookAt(0.02f,Vector3.forward);
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
