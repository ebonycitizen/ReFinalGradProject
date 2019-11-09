using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PathMoveCome : MonoBehaviour
{
    [SerializeField]
    private Transform pathRef;
    [SerializeField]
    private float moveTime;

    [SerializeField]
    private Transform glitter;

    [SerializeField]
    private Transform orca;

    private Sequence s;

    public bool hasDone { get; private set; }

    [SerializeField]
    private MyCinemachineDollyCart dollyCart;


    // Start is called before the first frame update
    void Start()
    {
        hasDone = false;

        //glitter.gameObject.SetActive(false);

        Vector3[] movePath = new Vector3[pathRef.childCount];

        for (int i = 0; i < movePath.Length; i++)
        {
            movePath[i] = pathRef.GetChild(i).position;
        }

        s = DOTween.Sequence();
        s.Join(transform.DOLocalPath(movePath, moveTime, PathType.CatmullRom)
           .SetEase(Ease.Linear)
           .SetLookAt(0.05f, Vector3.forward))
           .AppendCallback(() => hasDone = true)
           .AppendInterval(2f)
           .AppendCallback(() => glitter.gameObject.SetActive(true));
    }

    // Update is called once per frame
    void Update()
    {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0);

    }

    public void StartEvent()
    {
        SoundManager.Instance.PlayOneShot3DSe(ESeTable.Call, orca.GetComponentInChildren<Speaker>());
        s.Play();
    }
    public void EndEvent()
    {
        StartCoroutine("EventEnd");
    }
    private IEnumerator EventEnd()
    {
        float time = 10;
        while (time < 20)
        {

            dollyCart.m_Speed = time;
            time += Time.deltaTime * 3;
            yield return null;
        }

        dollyCart.m_Speed = 20f;

        yield return null;
    }
}
