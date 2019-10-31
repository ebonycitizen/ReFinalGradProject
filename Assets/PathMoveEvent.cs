using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PathMoveEvent : MonoBehaviour
{
    [SerializeField]
    private float moveTime;
    [SerializeField]
    private float delayTime;
    [SerializeField]
    private Transform pathRef;
    [SerializeField]
    private Ease ease;
    [SerializeField]
    private Transform Glitter;
    [SerializeField]
    private Transform cameraRig;

    [SerializeField]
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        Vector3[] movePath = new Vector3[pathRef.childCount];

        for (int i = 0; i < movePath.Length; i++)
        {
            movePath[i] = pathRef.GetChild(i).position;
        }

        transform.DOLocalPath(movePath, moveTime, PathType.CatmullRom)
            .SetOptions(true).SetEase(Ease.Linear)
            .SetLookAt(0.05f, Vector3.forward).SetLoops(-1);


    }

    // Update is called once per frame
    void Update()
    {
        Vector3 relativePos = (cameraRig.transform.position - this.transform.position).normalized;

        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0);
        Glitter.position = transform.position + relativePos*distance;
    }

    void DestroyObj()
    {
        //Destroy(gameObject);
    }
}
