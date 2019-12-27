using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PathMoveEvent : MonoBehaviour
{
    [SerializeField]
    private Transform pathRef;
    [SerializeField]
    private float moveTime;
   
    [SerializeField]
    private Transform pathRef2;
    [SerializeField]
    private float moveTime2;

    [SerializeField]
    private Transform glitter;
    [SerializeField]
    private Transform cameraRig;

    [SerializeField]
    private Transform orca;

    [SerializeField]
    private float distance;

    private Sequence s;
    private Sequence s2;

    private bool lockRotate=false;
    public bool canTouch { get; private set; }

    // Start is called before the first frame update
    void Start()
    {

        Vector3[] movePath = new Vector3[pathRef.childCount];

        for (int i = 0; i < movePath.Length; i++)
        {
            movePath[i] = pathRef.GetChild(i).position;
        }


        Vector3[] movePath2 = new Vector3[pathRef2.childCount];

        for (int i = 0; i < movePath2.Length; i++)
        {
            movePath2[i] = pathRef2.GetChild(i).position;
        }

        var rotation = Vector3.forward * 360;

        s = DOTween.Sequence();

        //s.Join(transform.DOBlendableLocalRotateBy(rotation, 1, RotateMode.FastBeyond360).OnComplete(()=>lockRotate=true))
        //    .Join(transform.DOLocalPath(movePath, moveTime, PathType.CatmullRom)
        //    .SetEase(Ease.Linear)
        //    .SetLookAt(0.05f, Vector3.forward))
        //    .AppendCallback(() => NextEvent());

        s.Join(transform.DOPath(movePath, moveTime, PathType.CatmullRom)
            .SetEase(Ease.Linear)
            .SetLookAt(0.05f, Vector3.forward))
            .AppendCallback(() => NextEvent())
            //.AppendInterval(2f)
            .AppendCallback(() => SoundManager.Instance.PlayOntShotSe(ESeTable.Sparkle_1))
            .AppendCallback(() => glitter.gameObject.SetActive(true));
            


        s2 = DOTween.Sequence();
        s2.Join(transform.DOPath(movePath2, moveTime2, PathType.CatmullRom)
            .SetOptions(true).SetEase(Ease.Linear)
            .SetLookAt(0.05f, Vector3.forward));

        //s.Play();
    }

    private void NextEvent()
    {
        s2.Play().SetLoops(-1);
        glitter.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 relativePos = (cameraRig.transform.position - orca.transform.position).normalized;

        //if (lockRotate)
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0);

        if (glitter != null)
            glitter.position = orca.position + relativePos * distance;
    }

    public void startEvent()
    {
        s.Play();
    }
}
