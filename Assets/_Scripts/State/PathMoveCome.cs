using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using BrunoMikoski.TextJuicer;
using SWS;

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

    [SerializeField]
    private GameObject tutorial_3;

    [SerializeField]
    private GameObject tutorial_4;

    [SerializeField]
    private ParticleSystem[] illusionFishes;

    private Sequence s;

    public bool hasDone { get; private set; }

    [SerializeField]
    private MyCinemachineDollyCart dollyCart;

    [SerializeField]
    private GameObject sendObj;
    private OrcaState orcaState;


    // Start is called before the first frame update
    void Start()
    {
        hasDone = false;

        InitUI();

        Vector3[] movePath = new Vector3[pathRef.childCount];

        for (int i = 0; i < movePath.Length; i++)
        {
            movePath[i] = pathRef.GetChild(i).position;
        }

        s = DOTween.Sequence();
        s.Join(transform.DOPath(movePath, moveTime, PathType.CatmullRom)
           .SetEase(Ease.Linear)
           .SetLookAt(0.05f, Vector3.forward))
           .AppendCallback(() => hasDone = true)
           .AppendCallback(() => UISeq1());

        orcaState = Object.FindObjectOfType<OrcaState>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0);

    }

    private void InitUI()
    {
        var textGroup1 = tutorial_3.GetComponentInChildren<CanvasGroup>();
        textGroup1.DOFade(0f, 0f);

        var textGroup2 = tutorial_4.GetComponentInChildren<CanvasGroup>();
        textGroup2.DOFade(0f, 0f);

    }

    private void UISeq1()
    {
        Sequence s_1 = DOTween.Sequence();

        var textGroup = tutorial_3.GetComponentInChildren<CanvasGroup>();
        var anim1 = tutorial_3.GetComponentInChildren<JuicedText>();
        var duration = 1f;

        s_1.Append(textGroup.DOFade(1f, duration))
            .AppendCallback(() => anim1.Play());

        s_1.Play();
    }

    private void UISeq2()
    {
        Sequence s_1 = DOTween.Sequence();

        var textGroup1 = tutorial_3.GetComponentInChildren<CanvasGroup>();
        var textGroup2 = tutorial_4.GetComponentInChildren<CanvasGroup>();

        var anim2 = tutorial_4.GetComponentInChildren<JuicedText>();

        var duration = 1f;

        s_1.Append(textGroup1.DOFade(0f, duration))
            .Join(textGroup2.DOFade(1f, duration))
            .AppendCallback(()=> StartCoroutine("ShowIllusionFish"))
            .AppendCallback(() => anim2.Play())
            .AppendInterval(1f)
            .AppendCallback(() => tutorial_3.SetActive(false))
            .AppendInterval(10f)
            .Append(textGroup2.DOFade(0f, duration))
            .AppendInterval(1f)
            .AppendCallback(() => tutorial_4.SetActive(false));

        s_1.Play();
    }

    private IEnumerator ShowIllusionFish()
    {
        foreach(var p in illusionFishes)
        {
            p.Play();
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void StartEvent()
    {
        SoundManager.Instance.PlayOneShot3DSe(ESeTable.Orac_5, orca.GetComponentInChildren<Speaker>());
        s.Play();
    }
    public void EndEvent()
    {
        StartCoroutine("EventEnd");
        
        bool hasChangeState = orcaState.ChangeState("G_Swim", sendObj);

        sendObj.GetComponent<splineMove>().StartMove();

        if (!hasChangeState)
            return;

        StartCoroutine("ChangeState");
    }
    private IEnumerator ChangeState()
    {
        yield return new WaitForSeconds(4f);
        {
            bool hasChangeState = orcaState.ChangeState("G_Idle", sendObj);
        }
    }
    private IEnumerator EventEnd()
    {
        UISeq2();
        yield return new WaitForSeconds(3f);
        float time = 10f;
        while (time < 10)
        {

            dollyCart.m_Speed = time;
            time += Time.deltaTime * 3f;
            yield return null;
        }

        dollyCart.m_Speed = 10f;

        yield return null;
    }
}
